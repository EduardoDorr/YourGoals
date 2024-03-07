using YourGoals.Core.Events;

namespace YourGoals.Domain.FinancialGoals.Events;

public sealed record FinancialGoalCompletedEvent(Guid Id) : IDomainEvent;