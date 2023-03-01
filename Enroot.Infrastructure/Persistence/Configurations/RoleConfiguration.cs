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

            rpb.HasData(new object[]
            {
                new { Value= PermissionEnum.CreateTask, RoleId=RoleId.Create(RoleEnum.TenantAdmin) },
                new { Value=PermissionEnum.ReviewTask, RoleId=RoleId.Create(RoleEnum.TenantAdmin) },
                new { Value=PermissionEnum.CreateAccount, RoleId=RoleId.Create(RoleEnum.TenantAdmin) },

                new { Value=PermissionEnum.ReviewTask, RoleId=RoleId.Create(RoleEnum.Moderator) },
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
                value => RoleId.Create((RoleEnum)value)
            );

        builder.Ignore(t => t.DbId);

        builder.SeedEnumValues((RoleEnum role) => Role.Create(RoleId.Create(role), role.GetEnumDescriptionOrName()).Value);
    }
}