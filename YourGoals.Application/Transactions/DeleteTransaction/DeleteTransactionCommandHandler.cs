using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Core.Repositories;
using YourGoals.Domain.Transactions.Errors;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Domain.FinancialGoals.Services;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Abstractions.Errors;

namespace YourGoals.Application.Transactions.DeleteTransaction;

public sealed class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IFinancialGoalService _financialGoalService;

    public DeleteTransactionCommandHandler(
        IUnitOfWork unitOfWork,
        IFinancialGoalRepository financialGoalRepository,
        ITransactionRepository transactionRepository,
        IFinancialGoalService financialGoalService)
    {
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;
        _transactionRepository = transactionRepository;
        _financialGoalService = financialGoalService;
    }

    public async Task<Result> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id, cancellationToken);

        var financialGoalResult = _financialGoalService.ReverseTransaction(transaction.FinancialGoal, transaction);

        if (!financialGoalResult.Success)
            return Result.Fail(new HttpStatusCodeError(financialGoalResult.Errors[0], HttpStatusCode.BadRequest));

        transaction.Deactivate();

        // I need to reverse the current amount
        _financialGoalRepository.Update(transaction.FinancialGoal);
        _transactionRepository.Update(transaction);

        var deleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!deleted)
        return Result.Fail(new HttpStatusCodeError(TransactionErrors.CannotBeDeleted, HttpStatusCode.InternalServerError));

        return Result.Ok();
    }
}
