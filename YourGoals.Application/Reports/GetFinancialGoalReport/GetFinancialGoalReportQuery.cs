using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.Reports.GetFinancialGoalReport;

public sealed record GetFinancialGoalReportQuery(Guid FinancialGoalId, string Email) : IRequest<Result>;