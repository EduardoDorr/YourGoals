using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using YourGoals.Core.Models;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Extensions;

namespace YourGoals.Infrastructure.Repositories;

public class FinancialGoalRepository : IFinancialGoalRepository
{
    private readonly YourGoalsDbContext _dbContext;

    public FinancialGoalRepository(YourGoalsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<FinancialGoal>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var financialGoals = _dbContext.FinancialGoals.AsQueryable();

        return await financialGoals.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<FinancialGoal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FinancialGoals.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<FinancialGoal?> GetSingleByAsync(Expression<Func<FinancialGoal, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FinancialGoals.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Create(FinancialGoal financialGoal)
    {
        _dbContext.FinancialGoals.Add(financialGoal);
    }

    public void Update(FinancialGoal financialGoal)
    {
        _dbContext.FinancialGoals.Update(financialGoal);
    }

    public void Delete(FinancialGoal financialGoal)
    {
        _dbContext.FinancialGoals.Remove(financialGoal);
    }
}