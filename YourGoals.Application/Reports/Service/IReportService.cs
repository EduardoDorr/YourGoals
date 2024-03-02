using YourGoals.Core.Results;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Application.Reports.Service;

public interface IReportService
{
    Result<string> GenerateFinancialGoalReport(FinancialGoal financialGoal);
}