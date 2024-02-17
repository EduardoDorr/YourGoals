using YourGoals.Core.Entities;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Domain.Transactions.Entities;

public class Transaction : BaseEntity
{
    public Guid FinancialGoalId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public TransactionType Type { get; private set; }

    public virtual FinancialGoal FinancialGoal { get; private set; }

    public Transaction(Guid financialGoalId, decimal amount, DateTime transactionDate, TransactionType type)
    {
        FinancialGoalId = financialGoalId;
        Amount = amount;
        TransactionDate = transactionDate;
        Type = type;
    }
}