using YourGoals.Core.Interfaces;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Domain.FinancialGoals.Interfaces;

public interface IFinancialGoalRepository
    : IReadableRepository<FinancialGoal>, ICreatableRepository<FinancialGoal>, IUpdatableRepository<FinancialGoal>, IDeletableRepository<FinancialGoal>
{
}