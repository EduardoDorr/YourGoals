using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Infrastructure.Configurations;

internal class FinancialGoalConfiguration : BaseEntityConfiguration<FinancialGoal>
{
    public override void Configure(EntityTypeBuilder<FinancialGoal> builder)
    {
        base.Configure(builder);

        builder.HasIndex(b => b.Name)
               .IsUnique();

        builder.Property(b => b.Name)
               .HasMaxLength(50);

        builder.Property(b => b.GoalAmount)
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(b => b.CurrentAmount)
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(b => b.InitialAmount)
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(b => b.InterestRate)
               .HasColumnType("numeric(5,2)");

        builder.Property(b => b.Deadline)
               .HasColumnType("datetime");

        builder.Property(b => b.IdealMonthlySaving)
               .HasColumnType("numeric(18,2)");

        builder.Property(b => b.CoverImage)
               .HasMaxLength(500);

        builder.Property(b => b.Status)
               .IsRequired();

        builder.HasMany(b => b.Transactions)
               .WithOne(t => t.FinancialGoal)
               .HasForeignKey(t => t.FinancialGoalId)
               .IsRequired();
    }
}