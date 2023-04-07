using Enroot.Domain.ReadEntities;
using Enroot.Domain.Role.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleRead>
{
    public void Configure(EntityTypeBuilder<RoleRead> builder)
    {
        ConfigureRoleTable(builder);
    }

    private static void ConfigureRoleTable(EntityTypeBuilder<RoleRead> builder)
    {
        builder.ToView("Roles");
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions).WithOne(rp => rp.Role);
    }
}