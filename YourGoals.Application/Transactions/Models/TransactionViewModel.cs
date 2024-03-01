using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Application.Transactions.Models;

public record TransactionViewModel(
    Guid Id,
    Guid FinancialGoalId,
    decimal Amount,
    DateTime TransactionDate,
    TransactionType Type);

public static class TransactionViewModelExtension
{
    public static TransactionViewModel ToViewModel(this Transaction transaction)
    {
        return new TransactionViewModel(
            transaction.Id,
            transaction.FinancialGoalId,
            transaction.Amount,
            transaction.TransactionDate,
            transaction.Type);
    }

    public static IEnumerable<TransactionViewModel> ToViewModel(this IEnumerable<Transaction> transactions)
    {
        return transactions is not null
             ? transactions.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}