using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.FinancialGoals.UpdateFinancialGoal;

public sealed record UpdateFinancialGoalCommand(
    Guid Id,
    string Name,
    decimal GoalAmount,
    decimal InitialAmount,
    decimal? InterestRate,
    DateTime? Deadline) : IRequest<Result>;