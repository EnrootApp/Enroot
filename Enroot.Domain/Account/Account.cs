using Enroot.Domain.Account.Events;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Account;

public sealed class Account : AggregateRoot<AccountId>
{
    public RoleId RoleId { get; private set; }
    public TenantId TenantId { get; }
    public UserId UserId { get; }

    private Account(AccountId id, UserId userId, TenantId tenantId, RoleId roleId) : base(id)
    {
        RoleId = roleId;
        TenantId = tenantId;
        UserId = userId;
    }

    public static ErrorOr<Account> Create(UserId userId, TenantId tenantId, RoleId roleId)
    {
        if (roleId is null)
        {
            return Errors.Role.NotFound;
        }

        if (userId is null)
        {
            return Errors.User.NotFound;
        }

        if (tenantId is null)
        {
            return Errors.Tenant.NotFound;
        }

        var accountId = AccountId.CreateUnique();

        var account = new Account(accountId, userId, tenantId, roleId);

        account.AddDomainEvent(new AccountCreatedDomainEvent(userId, tenantId, accountId));

        return account;
    }

    public override ErrorOr<Entity<AccountId>> Delete()
    {
        base.Delete();

        AddDomainEvent(new AccountDeletedDomainEvent(UserId, TenantId, Id));

        return this;
    }

    public override ErrorOr<Entity<AccountId>> Restore()
    {
        base.Restore();

        AddDomainEvent(new AccountCreatedDomainEvent(UserId, TenantId, Id));

        return this;
    }

    public ErrorOr<Account> SetRole(RoleId role)
    {
        RoleId = role;

        return this;
    }
}