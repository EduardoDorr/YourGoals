using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using YourGoals.Application.Transactions.Services;

namespace YourGoals.Application.BackgroundJobs;

internal sealed class ProcessInterestTransactionsJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ProcessInterestTransactionsJob> _logger;

    public ProcessInterestTransactionsJob(
        IServiceProvider serviceProvider,
        ILogger<ProcessInterestTransactionsJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var interestTransactionService = scope.ServiceProvider.GetRequiredService<IInterestTransactionService>();

            await interestTransactionService.Process(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }
}