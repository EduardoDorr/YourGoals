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
    private const double AVERAGE_DAYS_PER_MONTH = 30.416667;

    public static FinancialGoalBuilder GetFinancialGoalSimple()
    {
        var financialGoalBuilder = FinancialGoal.CreateBuilder(NAME, GOAL_AMOUNT);

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadline()
    {
        var financialGoalBuilder = GetFinancialGoalSimple();
        financialGoalBuilder.WithDeadline(DateTime.Now.AddDays(NUMBER_OF_MONTHS * AVERAGE_DAYS_PER_MONTH));

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadlineAndInitialAmount()
    {
        var financialGoalBuilder = GetFinancialGoalWithDeadline();
        financialGoalBuilder.WithInitialAmount(INITIAL_AMOUNT);

        return financialGoalBuilder;
    }

    public static FinancialGoalBuilder GetFinancialGoalWithDeadlineAndInitialAmountAndInterestRate()
    {
        var financialGoalBuilder = GetFinancialGoalWithDeadlineAndInitialAmount();
        financialGoalBuilder.WithInterestRate(INTEREST_RATE);

        return financialGoalBuilder;
    }
}