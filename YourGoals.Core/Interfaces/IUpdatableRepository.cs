using YourGoals.Core.Entities;

namespace YourGoals.Core.Interfaces;

public interface IUpdatableRepository<TEntity> where TEntity : BaseEntity
{
    void Update(TEntity entity);
}