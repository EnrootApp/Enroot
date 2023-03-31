using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Tenant;

public sealed class Tenant : AggregateRoot<TenantId>
{
    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    public TenantName Name { get; private set; }
    public string? LogoUrl { get; private set; }

    private Tenant() { }

    private Tenant(TenantId id, TenantName name, string? logoUrl) : base(id)
    {
        Name = name;
        LogoUrl = logoUrl;
    }

    public static ErrorOr<Tenant> Create(TenantId id, TenantName name, string? logoUrl)
    {
        if (id is null)
        {
            return Errors.Tenant.NotFound;
        }

        if (name is null)
        {
            return Errors.Tenant.NameInvalid;
        }

        return new Tenant(id, name, logoUrl);
    }

    public ErrorOr<Tenant> AddAccountId(AccountId id)
    {
        if (id is null)
        {
            return Errors.Account.NotFound;
        }

        if (_accountIds.Contains(id))
        {
            return Errors.Tenant.AccountExists;
        }

        _accountIds.Add(id);

        return this;
    }
}