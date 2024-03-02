using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Services;
using YourGoals.Domain.FinancialGoals.Services;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Errors;
using YourGoals.Domain.FinancialGoals.Errors;
using YourGoals.Domain.Transactions.Errors;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Core.Repositories;

namespace YourGoals.Application.Transactions.CreateTransaction;

public sealed class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionService _transactionService;
    private readonly IFinancialGoalService _financialGoalService;

    public CreateTransactionCommandHandler(
        IUnitOfWork unitOfWork,
        IFinancialGoalRepository financialGoalRepository,
        ITransactionRepository transactionRepository,
        ITransactionService transactionService,
        IFinancialGoalService financialGoalService)
    {
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;
        _transactionRepository = transactionRepository;
        _transactionService = transactionService;
        _financialGoalService = financialGoalService;
    }

    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.FinancialGoalId, cancellationToken);

        if (financialGoal is null)
            return Result.Fail<Guid>(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        var transactionResult = _transactionService.Create(financialGoal, request.Amount, request.TransactionDate, request.Type);

        if (!transactionResult.Success)
            return Result.Fail<Guid>(new HttpStatusCodeError(transactionResult.Errors[0], HttpStatusCode.BadRequest));

        var transaction = transactionResult.Value;

        var financialGoalResult = _financialGoalService.AddTransaction(financialGoal, transaction);

        if (!financialGoalResult.Success)
            return Result.Fail<Guid>(new HttpStatusCodeError(financialGoalResult.Errors[0], HttpStatusCode.BadRequest));

        // Do something with the financial goal's status information
        // Maybe create a notification to send a e-mail about the financial goal status completition

        _transactionRepository.Create(transaction);
        _financialGoalRepository.Update(financialGoal);

        var created = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!created)
            return Result.Fail<Guid>(new HttpStatusCodeError(TransactionErrors.CannotBeCreated, HttpStatusCode.InternalServerError));

        return Result.Ok(transaction.Id);
    }
}