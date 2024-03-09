using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using YourGoals.Core.Repositories;
using YourGoals.Domain.Transactions.Interfaces;
using YourGoals.Domain.FinancialGoals.Interfaces;
using YourGoals.Application.Abstractions.EmailApi;
using YourGoals.Infrastructure.EmailApi;
using YourGoals.Infrastructure.Contexts;
using YourGoals.Infrastructure.Repositories;
using YourGoals.Infrastructure.Interceptors;
using YourGoals.Infrastructure.BackgroundJobs;

namespace YourGoals.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
                .AddInterceptors()
                .AddRepositories()
                .AddUnitOfWork()
                .AddServices()
                .AddBackgroundJobs()
                .AddHttpClients(configuration);

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["YouGoalsConnectionString"];

        services.AddDbContext<YourGoalsDbContext>((sp, opts) =>
        {
            opts.UseSqlServer(connectionString)
                .AddInterceptors(
                    sp.GetRequiredService<PublishDomainEventsToOutBoxMessagesInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsToOutBoxMessagesInterceptor>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IFinancialGoalRepository, FinancialGoalRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailApi, WebMailApi>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddHostedService<ProcessOutboxMessagesJob>();

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