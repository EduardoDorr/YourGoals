using Microsoft.Extensions.DependencyInjection;

using YourGoals.Domain.Transactions.Services;
using YourGoals.Domain.FinancialGoals.Services;

public static class DomainModule
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ITransactionService, TransactionService>();
        services.AddTransient<IFinancialGoalService, FinancialGoalService>();

        return services;
    }
}