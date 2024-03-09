using MediatR;

using YourGoals.Core.Results;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Reports.GetTransactionsReport;

public sealed record GetTransactionsReportQuery(
    DateTime InitialDate,
    DateTime FinalDate) : IRequest<Result<IEnumerable<TransactionViewModel>>>;