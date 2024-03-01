using YourGoals.Core.Results.Errors;

namespace YourGoals.Domain.FinancialGoals.Errors;

public record FinancialGoalErrors(string Code, string Message) : IError
{
    public static readonly Error CannotBeCreated =
        new("FinancialGoal.CannotBeCreated", "Something went wrong and the FinancialGoal cannot be created");

    public static readonly Error CannotBeUpdated =
        new("FinancialGoal.CannotBeUpdated", "Something went wrong and the FinancialGoal cannot be updated");

    public static readonly Error CannotBeDeleted =
        new("FinancialGoal.CannotBeDeleted", "Something went wrong and the FinancialGoal cannot be deleted");

    public static readonly Error NotFound =
        new("FinancialGoal.NotFound", "Not found");

    public static readonly Error TransactionAmountIsNotValid =
        new("FinancialGoal.TransactionAmountIsNotValid", "Transaction amount is not valid");
}