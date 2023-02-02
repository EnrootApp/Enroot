using Enroot.Domain.Account.Events;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;

namespace Enroot.Domain.Account;

public sealed class Account : AggregateRoot<AccountId>
{
    public RoleId RoleId { get; }
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
            throw new ArgumentNullException(nameof(roleId));
        }

        if (tenantId is null)
        {
            throw new ArgumentNullException(nameof(tenantId));
        }

        var accountId = AccountId.CreateUnique();

        var account = new Account(accountId, userId, tenantId, roleId);

        account.AddDomainEvent(new AccountCreatedDomainEvent(userId, tenantId, accountId));

        return account;
    }
}