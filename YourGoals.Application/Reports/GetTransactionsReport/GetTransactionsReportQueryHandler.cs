using System.Linq.Expressions;

using MediatR;

using YourGoals.Core.Models;
using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Reports.GetTransactionsReport;

public sealed class GetTransactionsReportQueryHandler : IRequestHandler<GetTransactionsReportQuery, Result<PaginationResult<TransactionViewModel>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsReportQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<PaginationResult<TransactionViewModel>>> Handle(GetTransactionsReportQuery request, CancellationToken cancellationToken)
    {
        var paginationTransactions =
            await _transactionRepository.GetAllByAsync(
                DateBetweenFactory(request),
                request.Page,
                request.PageSize,
                cancellationToken);

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

    private static Expression<Func<Transaction, bool>> DateBetweenFactory(GetTransactionsReportQuery request)
    {
        return t => t.TransactionDate >= request.InitialDate &&
                    t.TransactionDate <= request.FinalDate;
    }
}