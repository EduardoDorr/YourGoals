using YourGoals.Core.Entities;

namespace YourGoals.Core.Repositories;

public interface IUpdatableRepository<TEntity> where TEntity : BaseEntity
{
    void Update(TEntity entity);
}