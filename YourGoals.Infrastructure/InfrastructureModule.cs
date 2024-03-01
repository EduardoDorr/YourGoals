using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using YourGoals.Core.Interfaces;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Repositories;

namespace YourGoals.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
                .AddRepositories()
                .AddUnitOfWork();

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["YouGoalsConnectionString"];

        services.AddDbContext<YourGoalsDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IFinancialGoalRepository, FinancialGoalRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        //services.AddTransient <IGenericRepository<FinancialGoalRepository>, FinancialGoalRepository();
        //services.AddTransient <IGenericRepository<TransactionRepository>, TransactionRepository();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}