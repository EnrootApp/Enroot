
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.TenantAggregate;

public class TenantTests
{
    [Fact]
    public void Create_Should_ReturnSuccess()
    {
        var tenant = Tenant.Tenant.Create(TenantId.CreateUnique(), TenantName.Create("great"));

        Assert.False(tenant.IsError);
    }

    [Fact]
    public void AddAccountId_Should_AddOnlyFirst()
    {
        var tenant = Tenant.Tenant.Create(TenantId.CreateUnique(), TenantName.Create("great"));

        var accountId = AccountId.CreateUnique();
        tenant.Value.AddAccountId(accountId);
        tenant.Value.AddAccountId(accountId);

        Assert.Distinct(tenant.Value.AccountIds);
    }

    [Fact]
    public void TenantName_Should_ThrowDomainException()
    {
        Assert.Throws<DomainException>(() => TenantName.Create("ab"));
        Assert.Throws<DomainException>(() => TenantName.Create("abc#"));
        Assert.Throws<DomainException>(() => TenantName.Create("abc$"));
        Assert.Throws<DomainException>(() => TenantName.Create("abc!"));
    }

    [Fact]
    public void TenantName_Should_NotThrowDomainException()
    {
        var tenantName = TenantName.Create("abc");
        Assert.NotNull(tenantName);
    }
}