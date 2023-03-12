using Enroot.Domain.Permission.ValueObjects;
using Enroot.Domain.Role.ValueObjects;

namespace Enroot.Domain.Unit.Tests.PermissionAggregate;

public class RoleIdTests
{
    [Fact]
    public void Create_ShouldSucceed()
    {
        var roleId = RoleId.Create(Role.Enums.RoleEnum.TenantAdmin);

        Assert.False(roleId.IsError);
    }

    [Fact]
    public void Create_ShouldFail()
    {
        var permissionId = RoleId.Create((Role.Enums.RoleEnum)int.MaxValue);

        Assert.True(permissionId.IsError);
    }
}