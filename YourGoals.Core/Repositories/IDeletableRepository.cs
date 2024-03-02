using YourGoals.Core.Entities;

namespace YourGoals.Core.Repositories;

public interface IDeletableRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(TEntity entity);
}