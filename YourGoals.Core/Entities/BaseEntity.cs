namespace YourGoals.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public bool Active { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Active = true;
    }

    public virtual void Activate()
    {
        Active = true;

        UpdatedAt = DateTime.Now;
    }

    public virtual void Deactivate()
    {
        Active = false;

        UpdatedAt = DateTime.Now;
    }
}