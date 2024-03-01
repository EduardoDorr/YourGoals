using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Transactions.GetTransactions;

public sealed class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, Result<PaginationResult<TransactionViewModel>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<PaginationResult<TransactionViewModel>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var paginationTransactions = await _transactionRepository.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        var transactionsViewModel = paginationTransactions.Data.ToViewModel();

        var paginationTransactionsViewModel =
            new PaginationResult<TransactionViewModel>
            (
                paginationTransactions.Page,
                paginationTransactions.PageSize,
                paginationTransactions.TotalCount,
                paginationTransactions.TotalPages,
                transactionsViewModel.ToList()
            );

        return Result.Ok(paginationTransactionsViewModel);
    }
}