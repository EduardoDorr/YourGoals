using System.Net;

using MediatR;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Errors;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Application.Transactions.Models;
using YourGoals.Application.Abstractions.Errors;

namespace YourGoals.Application.Transactions.GetTransaction;

public sealed class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Result<TransactionViewModel?>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<TransactionViewModel?>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (transaction is null)
            return Result.Fail<TransactionViewModel?>(new HttpStatusCodeError(TransactionErrors.NotFound, HttpStatusCode.NotFound));

        var transactionViewModel = transaction?.ToViewModel();

        return Result.Ok(transactionViewModel);
    }
}