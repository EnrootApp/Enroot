using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.Common;

public class EntityTests
{
    [Fact]
    public void EqualityTest()
    {
        var tenantId = TenantId.CreateUnique();

        var tenant = Tenant.Tenant.Create(tenantId, TenantName.Create("great"));
        var sameTenant = Tenant.Tenant.Create(tenantId, TenantName.Create("super"));

        Assert.False(tenant.IsError);
        Assert.False(sameTenant.IsError);

        Assert.Equal(tenant.Value, sameTenant.Value);
    }
}