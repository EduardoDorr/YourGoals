using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.FinancialGoals.Services;

namespace YourGoals.Tests.Unit.FinancialGoals;

public class FinancialGoalServiceTest
{
    [Fact]
    public void SimpleFinancialGoal_DepositIsMade_GoalIsNotAchieved()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalSimple();
        var financialGoal = financialGoalBuilder.Build();
        var transaction = TransactionBuilders.GetTransactionDeposit();

        IFinancialGoalService financialGoalService = new FinancialGoalService();

        // Act
        var result = financialGoalService.AddTransaction(financialGoal, transaction);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(FinancialGoalStatus.InProgress, financialGoal.Status);
    }

    [Fact]
    public void SimpleFinancialGoal_DepositIsMade_GoalIsAchieved()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalSimple();
        var financialGoal = financialGoalBuilder.Build();
        var transaction = TransactionBuilders.GetTransactionDeposit(51000.0m);

        IFinancialGoalService financialGoalService = new FinancialGoalService();

        // Act
        var result = financialGoalService.AddTransaction(financialGoal, transaction);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(FinancialGoalStatus.Completed, financialGoal.Status);
    }

    [Fact]
    public void SimpleFinancialGoal_WithdrawIsMadeWithoutEnoughCurrentAmount_ResultIsNotSuccess()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalSimple();
        var financialGoal = financialGoalBuilder.Build();
        var transaction = TransactionBuilders.GetTransactionWithdraw(1.0m);

        IFinancialGoalService financialGoalService = new FinancialGoalService();

        // Act
        var result = financialGoalService.AddTransaction(financialGoal, transaction);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
    }

    [Fact]
    public void FinancialGoalWithDeadline_DepositIsReversed_CurrentAmountShouldBeZero()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalWithDeadline();
        var financialGoal = financialGoalBuilder.Build();
        var transaction = TransactionBuilders.GetTransactionDeposit();

        var financialGoalService = new FinancialGoalService();
        financialGoalService.AddTransaction(financialGoal, transaction);

        // Act
        var result = financialGoalService.ReverseTransaction(financialGoal, transaction);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(0.0m, financialGoal.CurrentAmount);
    }

    [Fact]
    public void FinancialGoalWithDeadlineAndInitialAmount_WithdrawIsReversed_CurrentAmountShouldBeInitialAmount()
    {
        // Arrange
        var financialGoalBuilder = FinancialGoalBuilders.GetFinancialGoalWithDeadlineAndInitialAmount(5000.0m);
        var financialGoal = financialGoalBuilder.Build();
        var transaction = TransactionBuilders.GetTransactionWithdraw();

        var financialGoalService = new FinancialGoalService();
        financialGoalService.AddTransaction(financialGoal, transaction);

        // Act
        var result = financialGoalService.ReverseTransaction(financialGoal, transaction);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(5000.0m, financialGoal.CurrentAmount);
    }
}