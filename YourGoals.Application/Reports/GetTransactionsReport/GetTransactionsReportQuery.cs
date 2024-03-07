using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Reports.GetTransactionsReport;

public sealed record GetTransactionsReportQuery(
    DateTime InitialDate,
    DateTime FinalDate,
    int Page = 1,
    int PageSize = 10) : IRequest<Result<PaginationResult<TransactionViewModel>>>;