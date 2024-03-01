using MediatR;

using YourGoals.Core.Results;

namespace YourGoals.Application.Transactions.DeleteTransaction;

public sealed record DeleteTransactionCommand(Guid Id) : IRequest<Result>;