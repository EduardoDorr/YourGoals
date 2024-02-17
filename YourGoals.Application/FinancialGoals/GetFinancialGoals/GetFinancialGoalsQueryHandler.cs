using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.FinancialGoals.Models;

namespace YourGoals.Application.FinancialGoals.GetFinancialGoals;

public sealed class GetFinancialGoalsQueryHandler : IRequestHandler<GetFinancialGoalsQuery, Result<PaginationResult<FinancialGoalViewModel>>>
{
    private readonly IFinancialGoalRepository _financialGoalRepository;

    public GetFinancialGoalsQueryHandler(IFinancialGoalRepository financialGoalRepository)
    {
        _financialGoalRepository = financialGoalRepository;
    }

    public async Task<Result<PaginationResult<FinancialGoalViewModel>>> Handle(GetFinancialGoalsQuery request, CancellationToken cancellationToken)
    {
        var paginationFinancialGoals = await _financialGoalRepository.GetAllAsync(request.Page, request.PageSize);

        var financialGoalsViewModel = paginationFinancialGoals.Data.ToViewModel();

        var paginationFiancialGoalsViewModel =
            new PaginationResult<FinancialGoalViewModel>
            (
                paginationFinancialGoals.Page,
                paginationFinancialGoals.PageSize,
                paginationFinancialGoals.TotalCount,
                paginationFinancialGoals.TotalPages,
                financialGoalsViewModel.ToList()
            );

        return Result.Ok(paginationFiancialGoalsViewModel);
    }
}
