
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Unit.Tests.TenantAggregate;

public class TenantTests
{
    [Fact]
    public void AddAccountIdTest()
    {
        var tenant = Tenant.Tenant.Create(TenantId.CreateUnique(), TenantName.Create("great"));

        Assert.False(tenant.IsError);

        var accountId = AccountId.CreateUnique();
        tenant.Value.AddAccountId(accountId);
        tenant.Value.AddAccountId(accountId);

        Assert.Distinct(tenant.Value.AccountIds);
    }
}