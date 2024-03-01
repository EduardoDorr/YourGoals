using YourGoals.Core.Results.Errors;

namespace YourGoals.Domain.Transactions.Errors;

public record TransactionErrors(string Code, string Message) : IError
{
    public static readonly Error CannotBeCreated =
        new("Transaction.CannotBeCreated", "Something went wrong and the Transaction cannot be created");

    public static readonly Error CannotBeUpdated =
        new("Transaction.CannotBeUpdated", "Something went wrong and the Transaction cannot be updated");

    public static readonly Error CannotBeDeleted =
        new("Transaction.CannotBeDeleted", "Something went wrong and the Transaction cannot be deleted");

    public static readonly Error NotFound =
        new("Transaction.NotFound", "Not found");

    public static readonly Error FinancialGoalNotFound =
        new("Transaction.FinancialGoalNotFound", "Financial Goal not found");

    public static readonly Error FinancialGoalIsNotValid =
        new("Transaction.FinancialGoalIsNotValid", "Financial Goal is not in progress status");

    public static readonly Error AmountIsNotValid =
        new("Transaction.AmountIsNotValid", "Amount must be greater than 0");

    public static readonly Error TransactionDateIsNotValid =
        new("Transaction.TransactionDateIsNotValid", "Transaction Date cannot be earlier than today");
}