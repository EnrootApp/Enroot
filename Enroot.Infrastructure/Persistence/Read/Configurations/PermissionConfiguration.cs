using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionRead>
{
    public void Configure(EntityTypeBuilder<PermissionRead> builder)
    {
        ConfigurePermissionTable(builder);
    }

    private static void ConfigurePermissionTable(EntityTypeBuilder<PermissionRead> builder)
    {
        builder.ToView("Permissions");
    }
}