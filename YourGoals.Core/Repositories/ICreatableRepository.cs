using YourGoals.Core.Entities;

namespace YourGoals.Core.Repositories;

public interface ICreatableRepository<TEntity> where TEntity : BaseEntity
{
    void Create(TEntity entity);
}