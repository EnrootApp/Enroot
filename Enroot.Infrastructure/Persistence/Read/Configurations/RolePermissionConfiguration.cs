using Enroot.Domain.ReadEntities;
using Enroot.Domain.Role.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionRead>
{
    public void Configure(EntityTypeBuilder<RolePermissionRead> builder)
    {
        ConfigureRolePermissionTable(builder);
    }

    private static void ConfigureRolePermissionTable(EntityTypeBuilder<RolePermissionRead> builder)
    {
        builder.ToView("RolePermissions");
        builder.Property(rp => rp.RoleId).HasColumnName("RoleId");
        builder.HasKey(r => new { r.RoleId, r.PermissionId });
    }
}