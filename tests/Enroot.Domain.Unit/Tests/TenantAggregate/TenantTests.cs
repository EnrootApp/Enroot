
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.TenantAggregate;

public class TenantTests
{
    [Fact]
    public void Create_Should_ReturnSuccess()
    {
        var tenant = Tenant.Tenant.Create(TenantName.Create("great").Value, string.Empty);

        Assert.False(tenant.IsError);
    }

    [Fact]
    public void AddAccountId_Should_AddOnlyFirst()
    {
        var tenant = Tenant.Tenant.Create(TenantName.Create("great").Value, string.Empty);

        var accountId = AccountId.CreateUnique();
        tenant.Value.AddAccountId(accountId);
        tenant.Value.AddAccountId(accountId);

        Assert.Distinct(tenant.Value.AccountIds);
    }

    [Fact]
    public void TenantName_Should_ReturnError()
    {
        Assert.True(TenantName.Create("ab").IsError);
        Assert.True(TenantName.Create("abc#").IsError);
        Assert.True(TenantName.Create("abc$").IsError);
        Assert.True(TenantName.Create("abc!").IsError);
    }

    [Fact]
    public void TenantName_Should_NotRetunError()
    {
        var tenantName = TenantName.Create("abc").Value;
        Assert.NotNull(tenantName);
    }
}