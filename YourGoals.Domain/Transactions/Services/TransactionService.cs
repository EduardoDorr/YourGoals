using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Errors;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Domain.Transactions.Services;

public sealed class TransactionService : ITransactionService
{
    public Result<Transaction> Create(FinancialGoal? financialGoal, decimal amount, DateTime transactionDate, TransactionType transactionType)
    {
        var validateResult = ValidateInput(financialGoal, amount, transactionDate, transactionType);

        if (!validateResult.Success)
            return Result.Fail<Transaction>(validateResult.Errors);

        var transaction = new Transaction(financialGoal.Id, amount, transactionDate, transactionType);

        return Result<Transaction>.Ok(transaction);
    }

    private static Result ValidateInput(FinancialGoal? financialGoal, decimal amount, DateTime transactionDate, TransactionType transactionType)
    {
        var financialGoalValidateResult = ValidateFinancialGoal(financialGoal);

        if (!financialGoalValidateResult.Success)
            return Result.Fail(financialGoalValidateResult.Errors);

        var transactionInputValidateResult = ValidateTransactionInput(amount, transactionDate);

        if (!transactionInputValidateResult.Success)
            return Result.Fail(transactionInputValidateResult.Errors);

        return Result.Ok();
    }

    private static Result ValidateTransactionInput(decimal amount, DateTime transactionDate)
    {
        if (amount <= 0)
            return Result.Fail(TransactionErrors.AmountIsNotValid);

        if (transactionDate < DateTime.Today)
            return Result.Fail(TransactionErrors.TransactionDateIsNotValid);

        return Result.Ok();
    }

    private static Result ValidateFinancialGoal(FinancialGoal? financialGoal)
    {
        if (financialGoal is null)
            return Result.Fail(TransactionErrors.FinancialGoalNotFound);

        if (financialGoal.Status != FinancialGoalStatus.InProgress)
            return Result.Fail(TransactionErrors.FinancialGoalIsNotValid);

        return Result.Ok();
    }
}