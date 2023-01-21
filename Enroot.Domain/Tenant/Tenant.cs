using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;

namespace Enroot.Domain.Tenant;

public sealed class Tenant : AggregateRoot<TenantId>
{
    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    public string Name { get; }

    private Tenant(TenantId id, string name) : base(id)
    {
        Name = name;
    }

    public static Tenant Create(TenantId id, string name)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
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