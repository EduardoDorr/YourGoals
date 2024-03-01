using MediatR;

using YourGoals.Core.Results;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Transactions.GetTransaction;

public sealed record GetTransactionQuery(Guid Id) : IRequest<Result<TransactionViewModel?>>;