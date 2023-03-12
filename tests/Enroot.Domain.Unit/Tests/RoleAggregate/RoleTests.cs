using Enroot.Domain.Permission.ValueObjects;
using Enroot.Domain.Role.ValueObjects;

namespace Enroot.Domain.Unit.Tests.RoleAggregate;

public class RoleTests
{
    [Fact]
    public void Create_ShouldSucceed()
    {
        var roleId = RoleId.Create(Role.Enums.RoleEnum.Default).Value;
        var role = Role.Role.Create(roleId, "Default");

        Assert.False(role.IsError);
    }

    [Fact]
    public void Create_ShouldFailName()
    {
        var roleId = RoleId.Create(Role.Enums.RoleEnum.Default).Value;
        var role = Role.Role.Create(roleId, "");

        Assert.True(role.IsError);
    }

    [Fact]
    public void AddPermission_ShouldSucceed()
    {
        var roleId = RoleId.Create(Role.Enums.RoleEnum.Default).Value;
        var role = Role.Role.Create(roleId, "Default");

        var permission = Permission.Permission.Create(PermissionId.Create(Permission.Enums.PermissionEnum.CreateTasq).Value, "Sample");

        Assert.False(role.Value.AddPermission(permission.Value.Id).IsError);
    }

    [Fact]
    public void AddPermission_ShouldFail()
    {
        var roleId = RoleId.Create(Role.Enums.RoleEnum.Default).Value;
        var role = Role.Role.Create(roleId, "Default");

        var permission = Permission.Permission.Create(PermissionId.Create(Permission.Enums.PermissionEnum.CreateTasq).Value, "Sample");
        role.Value.AddPermission(permission.Value.Id);

        Assert.True(role.Value.AddPermission(permission.Value.Id).IsError);
    }
}