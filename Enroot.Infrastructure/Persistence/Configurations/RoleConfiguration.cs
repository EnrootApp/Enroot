using Enroot.Domain.Role;
using Enroot.Domain.Role.Entities;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        ConfigurePermissionTable(builder);
        ConfigureRolePermissionTable(builder);
    }

    private static void ConfigureRolePermissionTable(EntityTypeBuilder<Role> builder)
    {
        builder.OwnsMany(t => t.Permissions, rpb =>
        {
            rpb.ToTable("RolePermissions");

            rpb.WithOwner().HasForeignKey(nameof(RoleId));

            rpb.HasKey(rp => rp.Value).HasName("Id");
        });

        builder.Metadata.FindNavigation(nameof(Role.Permissions))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigurePermissionTable(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => (int)id.Value,
                value => RoleId.Create((RoleEnum)value)
            );

        builder.Ignore(t => t.DbId);

        builder.SeedEnumValues((RoleEnum role) => Role.Create(RoleId.Create(role), role.GetEnumDescriptionOrName()));
    }
}