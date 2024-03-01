using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Transactions.GetTransactions;

public sealed record GetTransactionsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<TransactionViewModel>>>;