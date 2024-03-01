using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using YourGoals.Core.Entities;

namespace YourGoals.Infrastructure.Configurations;

internal abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Active)
               .IsRequired();

        builder.HasIndex(b => b.Active);

        builder.Property(b => b.CreatedAt)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(b => b.UpdatedAt)
               .HasColumnType("datetime")
               .IsRequired();

        builder.Property(b => b.Year)
               .HasColumnType("smallint")
               .IsRequired();

        builder.Property(b => b.Month)
               .HasColumnType("tinyint")
               .IsRequired();

        builder.Property(b => b.Day)
               .HasColumnType("tinyint")
               .IsRequired();
    }
}