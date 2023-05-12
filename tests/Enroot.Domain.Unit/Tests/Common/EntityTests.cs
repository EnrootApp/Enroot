using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.Common;

public class EntityTests
{
    [Fact]
    public void EqualityTest()
    {
        var tenant = Tenant.Tenant.Create(TenantName.Create("great").Value, string.Empty);
        var sameTenant = Tenant.Tenant.Create(TenantName.Create("great123").Value, string.Empty);

        Assert.False(tenant.IsError);
        Assert.False(sameTenant.IsError);

        Assert.NotEqual(tenant.Value, sameTenant.Value);
    }
}