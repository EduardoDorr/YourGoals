using YourGoals.Core.Interfaces;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Domain.FinancialGoals.Interfaces;

public interface ITransactionRepository
    : IReadableRepository<Transaction>,
      ICreatableRepository<Transaction>,
      IUpdatableRepository<Transaction>,
      IDeletableRepository<Transaction>
{
}