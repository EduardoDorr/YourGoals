using YourGoals.Core.Events;
using YourGoals.Application.Abstractions.EmailApi;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Domain.FinancialGoals.Events;

namespace YourGoals.Application.Transactions.Events;

public sealed class FinancialGoalCompletedEventHandler : IDomainEventHandler<FinancialGoalCompletedEvent>
{
    private const string EMAIL = "edudorr@gmail.com";

    private readonly IFinancialGoalRepository _financialGoalRepository;
    private readonly IEmailApi _emailApi;

    public FinancialGoalCompletedEventHandler(IFinancialGoalRepository financialGoalRepository, IEmailApi emailApi)
    {
        _financialGoalRepository = financialGoalRepository;
        _emailApi = emailApi;
    }

    public async Task Handle(FinancialGoalCompletedEvent notification, CancellationToken cancellationToken)
    {
        var financialGoal = await _financialGoalRepository.GetByIdAsync(notification.Id, cancellationToken);

        if (financialGoal is null)
            return;

        var emailInputModel =
            new EmailInputModel(EMAIL, $"{financialGoal.Name} - Completado");

        await _emailApi.SendEmail(emailInputModel);
    }
}