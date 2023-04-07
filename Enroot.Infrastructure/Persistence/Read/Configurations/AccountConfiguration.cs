using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountRead>
{
    public void Configure(EntityTypeBuilder<AccountRead> builder)
    {
        ConfigureAccountTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<AccountRead> builder)
    {
        builder.ToView("Accounts");
        builder.Property(a => a.RoleId).HasColumnName("RoleId");
    }
}