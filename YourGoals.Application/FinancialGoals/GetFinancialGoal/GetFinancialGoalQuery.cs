using MediatR;

using YourGoals.Core.Results;
using YourGoals.Application.FinancialGoals.Models;

namespace YourGoals.Application.FinancialGoals.GetFinancialGoal;

public sealed record GetFinancialGoalQuery(Guid Id) : IRequest<Result<FinancialGoalViewModel?>>;