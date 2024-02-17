using System.Net;

using MediatR;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Core.Results;
using YourGoals.Application.Errors;
using YourGoals.Domain.FinancialGoals.DomainErrors;
using YourGoals.Application.FinancialGoals.Models;

namespace YourGoals.Application.FinancialGoals.GetFinancialGoal;

public sealed class GetFinancialGoalQueryHandler : IRequestHandler<GetFinancialGoalQuery, Result<FinancialGoalViewModel?>>
{
    private readonly IFinancialGoalRepository _financialGoalRepository;

    public GetFinancialGoalQueryHandler(IFinancialGoalRepository financialGoalRepository)
    {
        _financialGoalRepository = financialGoalRepository;
    }

    public async Task<Result<FinancialGoalViewModel?>> Handle(GetFinancialGoalQuery request, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(request.Id, cancellationToken);

        if (financialGoal is null)
            return Result.Fail<FinancialGoalViewModel?>(new HttpStatusCodeError(FinancialGoalErrors.NotFound, HttpStatusCode.NotFound));

        var financialGoalViewModel = financialGoal?.ToViewModel();

        return Result.Ok(financialGoalViewModel);
    }
}