using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.FinancialGoals.Errors;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Errors;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Domain.FinancialGoals.Services;

public class FinancialGoalService : IFinancialGoalService
{
    public Result AddTransaction(FinancialGoal? financialGoal, Transaction? transaction)
    {
        var validateResult = ValidateInput(financialGoal, transaction);

        if (!validateResult.Success)
            return Result.Fail(validateResult.Errors);

        if (transaction.Type != TransactionType.Withdraw)
            financialGoal.Deposit(transaction.Amount);
        else if (financialGoal.CurrentAmount < transaction.Amount)
            return Result.Fail(FinancialGoalErrors.TransactionAmountIsNotValid);
        else
            financialGoal.Withdraw(transaction.Amount);

        financialGoal.ValidateGoalAchievement();

        return Result.Ok();
    }

    public Result ReverseTransaction(FinancialGoal? financialGoal, Transaction? transaction)
    {
        var validateResult = ValidateInput(financialGoal, transaction);

        if (!validateResult.Success)
            return Result.Fail(validateResult.Errors);

        var interestPaid = DateTime.Today.Day >= financialGoal.Day;
        var isWithdraw = transaction.Type.Equals(TransactionType.Withdraw);

        var newAmount = GetNewAmount(financialGoal.CurrentAmount, transaction.Amount, financialGoal.InterestRate, interestPaid, isWithdraw);

        var amountDifference = newAmount - financialGoal.CurrentAmount;

        if (amountDifference > 0)
            financialGoal.Deposit(amountDifference);
        else
            financialGoal.Withdraw(-amountDifference);

        financialGoal.ValidateGoalAchievement();

        return Result.Ok();
    }

    private static decimal GetNewAmount(decimal currentAmount, decimal transactionAmount, decimal? interestRate, bool interestPaid, bool isWithdraw = false)
    {
        var newAmount = currentAmount;

        if (interestRate.HasValue &&
            interestPaid)
        {
            newAmount = currentAmount / (decimal)(1 + interestRate);
            newAmount += isWithdraw ? transactionAmount : -transactionAmount;
            newAmount *= (decimal)(1 + interestRate);
        }
        else
            newAmount += isWithdraw ? transactionAmount : -transactionAmount;

        return newAmount;
    }

    private static Result ValidateInput(FinancialGoal? financialGoal, Transaction? transaction)
    {
        if (financialGoal is null)
            return Result.Fail(FinancialGoalErrors.NotFound);

        if (transaction is null)
            return Result.Fail(TransactionErrors.NotFound);

        return Result.Ok();
    }
}