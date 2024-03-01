using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Core.Interfaces;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Errors;
using YourGoals.Domain.FinancialGoals.Errors;

namespace YourGoals.Application.FinancialGoals.UpdateFinancialGoal;

public sealed class UpdateFinancialGoalCommandHandler : IRequestHandler<UpdateFinancialGoalCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFinancialGoalRepository _financialGoalRepository;

    public UpdateFinancialGoalCommandHandler(IUnitOfWork unitOfWork, IFinancialGoalRepository financialGoalRepository)
    {
        _unitOfWork = unitOfWork;
        _financialGoalRepository = financialGoalRepository;
    }

    public async Task<Result> Handle(UpdateFinancialGoalCommand request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (financialGoal is null)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        financialGoal.Update(
            request.Name,
            request.InitialAmount,
            request.InterestRate,
            request.Deadline);

        _financialGoalRepository.Update(financialGoal);

        var updated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        if (!updated)
            return Result.Fail(new HttpStatusCodeError(FinancialGoalErrors.CannotBeUpdated, HttpStatusCode.NotFound));

        return Result.Ok();
    }
}