using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        ConfigureTenantTable(builder);
        ConfigureTenantAccountIdsTable(builder);
    }

    private void ConfigureTenantAccountIdsTable(EntityTypeBuilder<Tenant> builder)
    {
        builder.OwnsMany(t => t.AccountIds, accountIdBuilder =>
        {
            accountIdBuilder.ToTable("TenantAccountIds");

            accountIdBuilder.WithOwner().HasForeignKey(nameof(TenantId));

            accountIdBuilder.HasKey("Id");

            accountIdBuilder.Property(accountId => accountId.Value)
            .ValueGeneratedNever()
            .HasColumnName("AccountId");
        });

        builder.Metadata.FindNavigation(nameof(Tenant.AccountIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureTenantTable(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(t => t.DbId);

        builder
           .Property(t => t.DbId)
           .ValueGeneratedOnAdd();

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => TenantId.Create(value)
            );

        builder.OwnsOne(x => x.Name).Property(t => t.Value).HasColumnName("Name");
    }
}