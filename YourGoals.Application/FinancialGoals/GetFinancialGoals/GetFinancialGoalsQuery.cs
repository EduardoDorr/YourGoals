using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Application.FinancialGoals.Models;

namespace YourGoals.Application.FinancialGoals.GetFinancialGoals;

public sealed record GetFinancialGoalsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<FinancialGoalViewModel>>>;