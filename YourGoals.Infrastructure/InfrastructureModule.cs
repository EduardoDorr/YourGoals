using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using YourGoals.Core.Repositories;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Reports.Service;
using YourGoals.Infrastructure.MailApi;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Repositories;

namespace YourGoals.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
                .AddRepositories()
                .AddUnitOfWork()
                .AddServices()
                .AddHttpClients(configuration);

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

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IMailApi, WebMailApi>();

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("WebMailApi", client =>
        {
            client.BaseAddress = new Uri(configuration["WebMailAPI:Url"]);
        });

        return services;
    }
}