using YourGoals.Domain.FinancialGoals.Builder;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Tests.Utils.FinancialGoals;

public static class FinancialGoalBuilders
{
    public const string NAME = "Test";
    public const decimal GOAL_AMOUNT = 50000.0m;
    public const decimal INITIAL_AMOUNT = 1000.0m;
    public const decimal INTEREST_RATE = 1.0m;
    public const int NUMBER_OF_MONTHS = 40;

    public static FinancialGoalBuilder GetFinancialGoalSimple()
    {
        var financialGoalBuilder = FinancialGoal.CreateBuilder(NAME, GOAL_AMOUNT);

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadline(int numberOfMonths = NUMBER_OF_MONTHS)
    {
        var financialGoalBuilder = GetFinancialGoalSimple();
        financialGoalBuilder.WithDeadline(DateTime.Now.AddMonths(numberOfMonths));

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadlineAndInitialAmount(decimal initialAmount = INITIAL_AMOUNT)
    {
        var financialGoalBuilder = GetFinancialGoalWithDeadline();
        financialGoalBuilder.WithInitialAmount(initialAmount);

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadlineAndInitialAmountAndInterestRate(decimal interestRate = INTEREST_RATE)
    {
        var financialGoalBuilder = GetFinancialGoalWithDeadlineAndInitialAmount();
        financialGoalBuilder.WithInterestRate(interestRate);

        return financialGoalBuilder;
    }
}