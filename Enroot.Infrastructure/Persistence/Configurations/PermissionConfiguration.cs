using Enroot.Domain.Permission;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.Permission.ValueObjects;
using Enroot.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        ConfigurePermissionTable(builder);
    }

    private static void ConfigurePermissionTable(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => (int)id.Value,
                value => PermissionId.Create((PermissionEnum)value)
            );

        builder.Ignore(t => t.DbId);

        builder.SeedEnumValues((PermissionEnum permission) => Permission.Create(PermissionId.Create(permission), permission.GetEnumDescriptionOrName()));
    }
}