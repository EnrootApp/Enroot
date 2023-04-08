using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Write.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        ConfigureTenantTable(builder);
        ConfigureTenantAccountIdsTable(builder);
        ConfigureTenantTasqIdsTable(builder);
    }

    private static void ConfigureTenantAccountIdsTable(EntityTypeBuilder<Tenant> builder)
    {
        builder.OwnsMany(t => t.AccountIds, accountIdBuilder =>
        {
            accountIdBuilder.ToTable("TenantAccountIds");

            accountIdBuilder.WithOwner().HasPrincipalKey(t => t.Id);

            accountIdBuilder.Property<int>("DbId").ValueGeneratedOnAdd();
            accountIdBuilder.HasKey("DbId");

            accountIdBuilder.Property(accountId => accountId.Value)
                .ValueGeneratedNever()
                .HasColumnName("AccountId")
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(Tenant.AccountIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureTenantTasqIdsTable(EntityTypeBuilder<Tenant> builder)
    {
        builder.OwnsMany(t => t.TasqIds, accountIdBuilder =>
        {
            accountIdBuilder.ToTable("TenantTasqIds");

            accountIdBuilder.WithOwner().HasPrincipalKey(t => t.Id);

            accountIdBuilder.Property<int>("DbId").ValueGeneratedOnAdd();
            accountIdBuilder.HasKey("DbId");

            accountIdBuilder.Property(tasqId => tasqId.Value)
                .ValueGeneratedNever()
                .HasColumnName("TasqId")
                .IsRequired();
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

        builder.Property(t => t.LogoUrl);

        builder.OwnsOne(x => x.Name).Property(t => t.Value).HasColumnName("Name").HasMaxLength(62);
    }
}