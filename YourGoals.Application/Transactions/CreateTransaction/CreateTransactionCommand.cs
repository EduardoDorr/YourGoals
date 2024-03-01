using MediatR;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Enums;

namespace YourGoals.Application.Transactions.CreateTransaction;

public sealed record CreateTransactionCommand(
    Guid FinancialGoalId,
    decimal Amount,
    DateTime TransactionDate,
    TransactionType Type) : IRequest<Result<Guid>>;