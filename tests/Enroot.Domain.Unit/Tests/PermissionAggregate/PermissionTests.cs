using Enroot.Domain.Permission.ValueObjects;

namespace Enroot.Domain.Unit.Tests.PermissionAggregate;

public class PermissionTests
{
    [Fact]
    public void Create_ShouldSucceed()
    {
        var permissionId = PermissionId.Create(Permission.Enums.PermissionEnum.CreateTasq);
        var permission = Permission.Permission.Create(permissionId.Value, "Sample");

        Assert.False(permission.IsError);
    }

    [Fact]
    public void Create_ShouldFail()
    {
        var permission = Permission.Permission.Create(null, "Sample");

        Assert.True(permission.IsError);
    }

    [Fact]
    public void Create_ShouldFailDescription()
    {
        var permissionId = PermissionId.Create(Permission.Enums.PermissionEnum.CreateTasq);
        var permission = Permission.Permission.Create(permissionId.Value, "");

        Assert.True(permission.IsError);
    }
}