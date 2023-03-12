using Enroot.Domain.Permission.ValueObjects;

namespace Enroot.Domain.Unit.Tests.PermissionAggregate;

public class PermissionIdTests
{
    [Fact]
    public void Create_ShouldSucceed()
    {
        var permissionId = PermissionId.Create(Permission.Enums.PermissionEnum.CreateTasq);

        Assert.False(permissionId.IsError);
    }

    [Fact]
    public void Create_ShouldFail()
    {
        var permissionId = PermissionId.Create((Permission.Enums.PermissionEnum)int.MaxValue);

        Assert.True(permissionId.IsError);
    }
}