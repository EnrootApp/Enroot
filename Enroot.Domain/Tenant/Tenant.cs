using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;
using ErrorOr;

namespace Enroot.Domain.Tenant;

public sealed class Tenant : AggregateRoot<TenantId>
{
    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    public TenantName Name { get; private set; }

    private Tenant() { }

    private Tenant(TenantId id, TenantName name) : base(id)
    {
        Name = name;
    }

    public static ErrorOr<Tenant> Create(TenantId id, TenantName name)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (name is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return new Tenant(id, name);
    }

    public void AddAccountId(AccountId id)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (_accountIds.Contains(id))
        {
            throw new ArgumentException();
        }

        _accountIds.Add(id);
    }
}