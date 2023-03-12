using Enroot.Domain.Account.Events;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using AccountEntity = Enroot.Domain.Account.Account;

namespace Enroot.Domain.Unit.Tests.AccountAggregate;

public class AccountTests
{
    [Fact]
    public void Create_ShouldSucceed()
    {
        var account = AccountEntity.Create(UserId.CreateUnique(), TenantId.CreateUnique(), RoleId.Create(Role.Enums.RoleEnum.Default).Value);

        Assert.False(account.IsError);
    }

    [Fact]
    public void Create_ShouldAddDomainEvent()
    {
        var account = AccountEntity.Create(UserId.CreateUnique(), TenantId.CreateUnique(), RoleId.Create(Role.Enums.RoleEnum.Default).Value);

        Assert.True(account.Value.DomainEvents.First() is AccountCreatedDomainEvent);
    }
}