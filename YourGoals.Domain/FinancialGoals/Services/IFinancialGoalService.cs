using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Domain.FinancialGoals.Services;

public interface IFinancialGoalService
{
    Result AddTransaction(FinancialGoal? financialGoal, Transaction? transaction);
    Result ReverseTransaction(FinancialGoal? financialGoal, Transaction? transaction);
}