namespace YourGoals.Application.Transactions.Services;

internal interface IInterestTransactionService
{
    Task Process(CancellationToken cancellationToken = default);
}