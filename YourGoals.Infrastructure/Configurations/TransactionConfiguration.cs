using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YourGoals.Domain.Transactions.Entities;

namespace YourGoals.Infrastructure.Configurations;

internal class TransactionConfiguration : BaseEntityConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.FinancialGoalId)
               .IsRequired();

        builder.Property(b => b.Amount)
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(b => b.TransactionDate)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(b => b.Type)
               .IsRequired();
    }
}