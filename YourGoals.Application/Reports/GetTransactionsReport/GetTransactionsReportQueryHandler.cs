using System.Linq.Expressions;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Application.Transactions.Models;

namespace YourGoals.Application.Reports.GetTransactionsReport;

public sealed class GetTransactionsReportQueryHandler : IRequestHandler<GetTransactionsReportQuery, Result<IEnumerable<TransactionViewModel>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsReportQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<IEnumerable<TransactionViewModel>>> Handle(GetTransactionsReportQuery request, CancellationToken cancellationToken)
    {
        var transactions =
            await _transactionRepository.GetAllByAsync(
                DateBetweenFactory(request),
                cancellationToken);

        var transactionsViewModel = transactions.ToViewModel();

        return Result.Ok(transactionsViewModel);
    }

    private static Expression<Func<Transaction, bool>> DateBetweenFactory(GetTransactionsReportQuery request)
    {
        return t => t.TransactionDate >= request.InitialDate &&
                    t.TransactionDate <= request.FinalDate;
    }
}