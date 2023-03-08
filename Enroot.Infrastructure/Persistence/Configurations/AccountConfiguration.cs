using Enroot.Domain.Account;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User;
using Enroot.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ConfigureAccountTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(t => t.DbId);

        builder
           .Property(a => a.TenantId)
           .HasConversion(
               ti => ti!.Value,
               value => TenantId.Create(value)
           );

        builder
           .Property(a => a.UserId)
           .HasConversion(
               ui => ui!.Value,
               value => UserId.Create(value)
           );

        builder
           .Property(a => a.RoleId)
           .HasConversion(
               ri => ri!.Value,
               value => RoleId.Create(value).Value
           );

        builder.HasOne<Tenant>().WithMany().HasPrincipalKey(t => t.Id);
        builder.HasOne<User>().WithMany().HasPrincipalKey(u => u.Id);
        builder.HasOne<Role>().WithMany().HasPrincipalKey(r => r.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AccountId.Create(value)
            );

        builder.HasIndex(a => new { a.TenantId, a.UserId }).IsUnique();
    }
}