using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Domain.Transactions.Services;

public interface ITransactionService
{
    Result<Transaction> Create(FinancialGoal? financialGoal, decimal amount, DateTime transactionDate, TransactionType transactionType);
}