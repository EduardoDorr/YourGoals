using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.FinancialGoals.DeleteFinancialGoal;

public sealed record DeleteFinancialGoalCommand(Guid Id) : IRequest<Result>;