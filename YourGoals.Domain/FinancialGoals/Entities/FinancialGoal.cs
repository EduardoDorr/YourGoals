using YourGoals.Core.Entities;
using YourGoals.Core.Interfaces;
using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.FinancialGoals.Builder;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Domain.FinancialGoals.Entities;

public class FinancialGoal : BaseEntity, IAggregateRoot
{
    private const double AVERAGE_DAYS_PER_MONTH = 30.416667;
    private decimal? _idealMonthlySaving;

    public string Name { get; private set; }
    public decimal GoalAmount { get; private set; }
    public decimal CurrentAmount { get; private set; }
    public decimal InitialAmount { get; private set; }
    public FinancialGoalStatus Status { get; private set; }
    public decimal? InterestRate { get; private set; }
    public DateTime? Deadline { get; private set; }
    public decimal? IdealMonthlySaving { get; init; }

    public virtual ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public static FinancialGoalBuilder CreateBuilder(string name, decimal goalAmount) => new(name, goalAmount);

    public FinancialGoal(string name, decimal goalAmount, decimal initialAmount, decimal? interestRate, DateTime? deadline)
    {
        Name = name;
        GoalAmount = goalAmount;
        CurrentAmount = initialAmount;
        InitialAmount = initialAmount;
        InterestRate = interestRate;
        Deadline = deadline;
        Status = FinancialGoalStatus.InProgress;

        IdealMonthlySaving = GetIdealMonthlySaving();
    }

    public void Update(string name, decimal initialAmount, decimal? interestRate, DateTime? deadline)
    {
        Name = name;
        InitialAmount = initialAmount;
        InterestRate = interestRate;
        Deadline = deadline;

        UpdatedAt = DateTime.Now;
    }

    private decimal? GetIdealMonthlySaving()
    {
        if (Deadline is null)
            return null;

        _idealMonthlySaving = 0.0m;

        if (InterestRate is null)
            _idealMonthlySaving = (GoalAmount - InitialAmount) / GetNumberOfMonths();
        else
            _idealMonthlySaving = GetInterestInPercentage() * (GoalAmount - InitialAmount * GetInterestByMonths()) / (GetInterestByMonths() - 1);

        return Math.Round((decimal)_idealMonthlySaving, 2);

        decimal GetNumberOfMonths() =>
            (decimal)((Deadline - CreatedAt.Date).Value.TotalDays / AVERAGE_DAYS_PER_MONTH);

        decimal GetInterestByMonths() =>
            (decimal)Math.Pow((double)(1 + GetInterestInPercentage()), (double)GetNumberOfMonths());

        decimal GetInterestInPercentage() =>
            (decimal)(InterestRate / 100);
    }
}