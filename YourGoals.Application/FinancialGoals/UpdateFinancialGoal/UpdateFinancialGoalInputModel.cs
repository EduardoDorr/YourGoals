namespace YourGoals.Application.FinancialGoals.UpdateFinancialGoal;

public sealed record UpdateFinancialGoalInputModel(
    string Name,
    decimal GoalAmount,
    decimal InitialAmount,
    decimal? InterestRate,
    DateTime? Deadline);