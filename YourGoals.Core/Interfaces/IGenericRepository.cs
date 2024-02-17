using YourGoals.Core.Entities;

namespace YourGoals.Core.Interfaces;

public interface IGenericRepository<TEntity>
    : IReadableRepository<TEntity>,
      ICreatableRepository<TEntity>,
      IUpdatableRepository<TEntity>,
      IDeletableRepository<TEntity> where TEntity : BaseEntity
{
}