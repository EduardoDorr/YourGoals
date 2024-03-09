using System.Linq.Expressions;

using Microsoft.Extensions.Logging;

using MediatR;

using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.FinancialGoals.Enums;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Transactions.CreateTransaction;

namespace YourGoals.Application.Transactions.Services;

internal class InterestTransactionService : IInterestTransactionService
{
    private readonly IFinancialGoalRepository _financialGoalRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<InterestTransactionService> _logger;

    public InterestTransactionService(
        IFinancialGoalRepository financialGoalRepository,
        IMediator mediator,
        ILogger<InterestTransactionService> logger)
    {
        _financialGoalRepository = financialGoalRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Process(CancellationToken cancellationToken = default)
    {
        var processDate = DateTime.Today;

        var financialGoals =
            await _financialGoalRepository
            .GetAllByAsync(GetPredicateForInterestDueDate(processDate), cancellationToken);

        var tasks =
            financialGoals
            .Select(async financialGoal => { await ProcessInterest(financialGoal, processDate); });

        await Task.WhenAll(tasks);
    }

    private async Task ProcessInterest(FinancialGoal financialGoal, DateTime processDate)
    {
        _logger.LogInformation($"Start processing of Financial Goal {financialGoal.Id} at {DateTime.Now}");

        var amount = financialGoal.GetInterestAmount();

        var command = new CreateTransactionCommand(financialGoal.Id, amount, processDate, TransactionType.Interest);

        var result = await _mediator.Send(command);

        if (!result.Success)
            _logger.LogError($"Something goes wrong with the Financial Goal {financialGoal.Id} :: {result.Errors.First().Code} :: {result.Errors.First().Message}");

        _logger.LogInformation($"Finish processing of Financial Goal {financialGoal.Id} at {DateTime.Now}");
    }

    private static Expression<Func<FinancialGoal, bool>> GetPredicateForInterestDueDate(DateTime processDate)
    {
        return f => (f.Year != processDate.Year || f.Month != processDate.Month) &&
                    f.Day == processDate.Day &&
                    f.Status == FinancialGoalStatus.InProgress &&
                    f.InterestRate.HasValue &&
                    f.Active;
    }
}