using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;

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

    public static Account Create(UserId userId, TenantId tenantId, RoleId roleId)
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

        // send domain event with created Id

        return new(accountId, userId, tenantId, roleId);
    }
}