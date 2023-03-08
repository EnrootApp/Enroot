using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.Permission.ValueObjects;
using Enroot.Domain.Role;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Infrastructure.Persistence.Common;

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

            rpb.HasKey(nameof(PermissionId.Value), nameof(RoleId));

            var tenantAdminRole = RoleId.Create(RoleEnum.TenantAdmin).Value;
            var moderatorRole = RoleId.Create(RoleEnum.Moderator).Value;
            var defaultRole = RoleId.Create(RoleEnum.Default).Value;

            rpb.HasData(new object[]
            {
                new { Value= PermissionEnum.CreateTasq, RoleId=tenantAdminRole },
                new { Value=PermissionEnum.ReviewTasq, RoleId=tenantAdminRole },
                new { Value=PermissionEnum.CreateAccount, RoleId=tenantAdminRole },

                new { Value=PermissionEnum.ReviewTasq, RoleId=moderatorRole },

                new { Value=PermissionEnum.CompleteTasq, RoleId=defaultRole },
            });
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
                value => RoleId.Create((RoleEnum)value).Value
            );

        builder.Ignore(t => t.DbId);

        builder.SeedEnumValues((RoleEnum role) => Role.Create(RoleId.Create(role).Value, role.GetEnumDescriptionOrName()).Value);
    }
}