using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Application.FinancialGoals.Models;

public record FinancialGoalViewModel(
    Guid Id,
    string Name,
    decimal GoalAmount,
    decimal CurrentAmount,
    decimal InitialAmount,
    decimal? InterestRate,
    DateTime? Deadline,
    decimal? IdealMonthlySaving,
    FinancialGoalStatus Status,
    IReadOnlyCollection<Transaction>? Transactions);

public static class FinancialGoalViewModelExtension
{
    public static FinancialGoalViewModel ToViewModel(this FinancialGoal financialGoal)
    {
        if (financialGoal.Transactions.Count != 0)
            return new FinancialGoalViewModel(
                financialGoal.Id,
                financialGoal.Name,
                financialGoal.GoalAmount,
                financialGoal.CurrentAmount,
                financialGoal.InitialAmount,
                financialGoal.InterestRate,
                financialGoal.Deadline,
                financialGoal.IdealMonthlySaving,
                financialGoal.Status,
                financialGoal.Transactions);

        return new FinancialGoalViewModel(
            financialGoal.Id,
            financialGoal.Name,
            financialGoal.GoalAmount,
            financialGoal.CurrentAmount,
            financialGoal.InitialAmount,
            financialGoal.InterestRate,
            financialGoal.Deadline,
            financialGoal.IdealMonthlySaving,
            financialGoal.Status,
            null);
    }

    public static IEnumerable<FinancialGoalViewModel> ToViewModel(this IEnumerable<FinancialGoal> financialGoals)
    {
        return financialGoals is not null
             ? financialGoals.Select(fg => fg.ToViewModel()).ToList()
             : [];
    }
}