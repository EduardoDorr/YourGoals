using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using YourGoals.Core.Models;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Extensions;
using YourGoals.Domain.Transactions.Interfaces;

namespace YourGoals.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly YourGoalsDbContext _dbContext;

    public TransactionRepository(YourGoalsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Transaction>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var transactions = _dbContext.Transactions.AsQueryable();

        return await transactions.GetPaged(page, pageSize, cancellationToken);
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Transactions.Include(t => t.FinancialGoal).SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Transaction?> GetSingleByAsync(Expression<Func<Transaction, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Transactions.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public void Create(Transaction financialGoal)
    {
        _dbContext.Transactions.Add(financialGoal);
    }

    public void Update(Transaction transaction)
    {
        _dbContext.Transactions.Update(transaction);
    }

    public void Delete(Transaction transaction)
    {
        _dbContext.Transactions.Remove(transaction);
    }
}