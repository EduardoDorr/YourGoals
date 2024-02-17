using MediatR;
using YourGoals.Core.Results;

namespace YourGoals.Application.FinancialGoals.CreateFinancialGoal;

public sealed record CreateFinancialGoalCommand(
    string Name,
    decimal GoalAmount,
    decimal InitialAmount,
    decimal? InterestRate,
    DateTime? Deadline) : IRequest<Result<Guid>>;