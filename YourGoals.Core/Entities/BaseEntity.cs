using YourGoals.Core.Events;

namespace YourGoals.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; protected set; }
    public int Year { get; init; }
    public int Month { get; init; }
    public int Day { get; init; }
    public bool Active { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;

        Year = CreatedAt.Year;
        Month = CreatedAt.Month;
        Day = CreatedAt.Day;

        Active = true;
    }

    public virtual void Activate()
        => Active = true;

    public virtual void Deactivate()
        => Active = false;

    public virtual void SetUpdatedAtDate(DateTime updatedAtDate)
        => UpdatedAt = updatedAtDate;

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}