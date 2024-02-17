using YourGoals.Core.Entities;

namespace YourGoals.Core.Interfaces;

public interface IDeletableRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(TEntity entity);
}