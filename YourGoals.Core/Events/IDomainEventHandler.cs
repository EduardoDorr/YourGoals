using MediatR;

namespace YourGoals.Core.Events;

public interface IDomainEventHandler<TNotification>
    : INotificationHandler<TNotification> where TNotification : INotification
{
}
