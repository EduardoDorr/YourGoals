using YourGoals.Domain.FinancialGoals.Enums;

namespace YourGoals.Tests.Unit.FinancialGoals;

public class FinancialGoalTest
{
    [Fact]
    public void SimpleFinancialGoal_GetBuild_IsCreated()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalSimple();

        // Act
        var financialGoal = financialGoalBuilder.Build();

        // Assert
        Assert.NotNull(financialGoal);
        Assert.True(financialGoal.Active);
        Assert.Equal(0.0m, financialGoal.InitialAmount);
        Assert.Equal(FinancialGoalBuilders.NAME, financialGoal.Name);
        Assert.Equal(FinancialGoalBuilders.GOAL_AMOUNT, financialGoal.GoalAmount);
        Assert.Equal(FinancialGoalStatus.InProgress, financialGoal.Status);
        Assert.Null(financialGoal.Deadline);
        Assert.Null(financialGoal.InterestRate);
        Assert.Null(financialGoal.IdealMonthlySaving);
    }

    [Fact]
    public void FinancialGoalWithDeadline_GetBuild_IsCreatedAndHasIdealMonthlySaving()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalWithDeadline();

        // Act
        var financialGoal = financialGoalBuilder.Build();

        // Assert
        Assert.NotNull(financialGoal);
        Assert.True(financialGoal.Active);
        Assert.Equal(0.0m, financialGoal.InitialAmount);
        Assert.Equal(FinancialGoalBuilders.NAME, financialGoal.Name);
        Assert.Equal(FinancialGoalBuilders.GOAL_AMOUNT, financialGoal.GoalAmount);
        Assert.Equal(FinancialGoalStatus.InProgress, financialGoal.Status);
        Assert.NotNull(financialGoal.Deadline);
        Assert.Equal(1250.00m, financialGoal.IdealMonthlySaving);
        Assert.Null(financialGoal.InterestRate);
    }

    [Fact]
    public void FinancialGoalWithDeadlineAndInitialAmount_GetBuild_IsCreatedAndHasIdealMonthlySaving()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalWithDeadlineAndInitialAmount();

        // Act
        var financialGoal = financialGoalBuilder.Build();

        // Assert
        Assert.NotNull(financialGoal);
        Assert.True(financialGoal.Active);
        Assert.Equal(1000.0m, financialGoal.InitialAmount);
        Assert.Equal(FinancialGoalBuilders.NAME, financialGoal.Name);
        Assert.Equal(FinancialGoalBuilders.GOAL_AMOUNT, financialGoal.GoalAmount);
        Assert.Equal(FinancialGoalStatus.InProgress, financialGoal.Status);
        Assert.NotNull(financialGoal.Deadline);
        Assert.Equal(1225.00m, financialGoal.IdealMonthlySaving);
        Assert.Null(financialGoal.InterestRate);
    }

    [Fact]
    public void FinancialGoalWithDeadlineAndInitialAmountAndInterestRate_GetBuild_IsCreatedAndHasIdealMonthlySaving()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalWithDeadlineAndInitialAmountAndInterestRate();

        // Act
        var financialGoal = financialGoalBuilder.Build();

        // Assert
        Assert.NotNull(financialGoal);
        Assert.True(financialGoal.Active);
        Assert.Equal(1000.0m, financialGoal.InitialAmount);
        Assert.Equal(FinancialGoalBuilders.NAME, financialGoal.Name);
        Assert.Equal(FinancialGoalBuilders.GOAL_AMOUNT, financialGoal.GoalAmount);
        Assert.Equal(FinancialGoalStatus.InProgress, financialGoal.Status);
        Assert.NotNull(financialGoal.Deadline);
        Assert.Equal(992.32m, financialGoal.IdealMonthlySaving);
        Assert.Equal(1.0m, financialGoal.InterestRate);
    }
}