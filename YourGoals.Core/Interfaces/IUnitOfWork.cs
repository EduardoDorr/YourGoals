namespace YourGoals.Core.Interfaces;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}