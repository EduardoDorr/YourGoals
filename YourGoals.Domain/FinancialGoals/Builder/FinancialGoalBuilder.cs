using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Domain.FinancialGoals.Builder;

public class FinancialGoalBuilder
{
    private string _name;
    private decimal _goalAmount;
    private decimal _initialAmount;
    private decimal? _interestRate;
    private DateTime? _deadline;

    public FinancialGoalBuilder(string name, decimal goalAmount)
    {
        _name = name;
        _goalAmount = goalAmount;
    }

    public FinancialGoalBuilder WithInitialAmount(decimal initialAmount)
    {
        _initialAmount = initialAmount;

        return this;
    }

    public FinancialGoalBuilder WithDeadline(DateTime? deadline)
    {
        _deadline = deadline;

        return this;
    }    

    public FinancialGoalBuilder WithInterestRate(decimal? interestRate)
    {
        _interestRate = interestRate;

        return this;
    }

    public FinancialGoal Build()
    {
        return new FinancialGoal(_name, _goalAmount, _initialAmount, _interestRate, _deadline);
    }
}