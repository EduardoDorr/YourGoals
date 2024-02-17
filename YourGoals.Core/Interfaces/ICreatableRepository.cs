using YourGoals.Core.Entities;

namespace YourGoals.Core.Interfaces;

public interface ICreatableRepository<TEntity> where TEntity : BaseEntity
{
    void Create(TEntity entity);
}