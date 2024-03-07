using System.Reflection;

using Microsoft.EntityFrameworkCore;

using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.FinancialGoals.Entities;
using YourGoals.Infrastructure.Outbox;

namespace YourGoals.Infrastructure.Contexts;

public class YourGoalsDbContext : DbContext
{
    public DbSet<FinancialGoal> FinancialGoals { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public YourGoalsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}