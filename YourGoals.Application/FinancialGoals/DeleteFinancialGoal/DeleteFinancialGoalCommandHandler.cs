using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Core.Repositories;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Domain.FinancialGoals.Errors;
using YourGoals.Application.Abstractions.Errors;


namespace YourGoals.Application.FinancialGoals.DeleteFinancialGoal;

public sealed class DeleteFinancialGoalCommandHandler : IRequestHandler<DeleteFinancialGoalCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;

    public DeleteFinancialGoalCommandHandler(IUnitOfWork unitOfWork, IFinancialGoalRepository financialGoalRepository)
    {
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;
    }

    public async Task<Result> Handle(DeleteFinancialGoalCommand request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (financialGoal is null)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        financialGoal.Deactivate();

        _financialGoalRepository.Update(financialGoal);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.CannotBeDeleted, HttpStatusCode.NotFound));

        return Result.Ok();
    }
}