using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.Common;

public class EntityTests
{
    [Fact]
    public void EqualityTest()
    {
        var tenantId = TenantId.CreateUnique();

        var tenant = Tenant.Tenant.Create(TenantId.CreateUnique(), TenantName.Create("great").Value, string.Empty);
        var sameTenant = Tenant.Tenant.Create(TenantId.CreateUnique(), TenantName.Create("great").Value, string.Empty);

        Assert.False(tenant.IsError);
        Assert.False(sameTenant.IsError);

        Assert.Equal(tenant.Value, sameTenant.Value);
    }
}