using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using YourGoals.Core.Models;
using YourGoals.Core.Entities;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Extensions;
using YourGoals.Core.Repositories;

namespace YourGoals.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly YourGoalsDbContext _dbContext;

    public GenericRepository(YourGoalsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<TEntity>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var entities = _dbContext.Set<TEntity>().AsQueryable();

        return await entities.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<TEntity?> GetSingleByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Create(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
}
