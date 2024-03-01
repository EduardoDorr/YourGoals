using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Tests.Utils.Transactions;

public static class TransactionBuilders
{
    public static Transaction GetTransactionDeposit(decimal amount = 1000.0m)
    {
        var transaction = new Transaction(new Guid(), amount, DateTime.Now, TransactionType.Deposit);

        return transaction;
    }

    public static Transaction GetTransactionWithdraw(decimal amount = 1000.0m)
    {
        var transaction = new Transaction(new Guid(), amount, DateTime.Now, TransactionType.Withdraw);

        return transaction;
    }
}