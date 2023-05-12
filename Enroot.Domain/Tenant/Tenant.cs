using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using Enroot.Domain.Tasq.ValueObjects;

namespace Enroot.Domain.Tenant;

public sealed class Tenant : AggregateRoot<TenantId>
{
    private readonly List<AccountId> _accountIds = new();
    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    private readonly List<TasqId> _tasqIds = new();
    public IReadOnlyList<TasqId> TasqIds => _tasqIds.AsReadOnly();

    public TenantName Name { get; private set; }
    public string? LogoUrl { get; private set; }

    private Tenant() { }

    private Tenant(TenantId id, TenantName name, string? logoUrl) : base(id)
    {
        Name = name;
        LogoUrl = logoUrl;
    }

    public static ErrorOr<Tenant> Create(TenantName name, string? logoUrl)
    {
        if (name is null)
        {
            return Errors.Tenant.NameInvalid;
        }

        return new Tenant(TenantId.CreateUnique(), name, logoUrl);
    }

    public ErrorOr<Tenant> Update(TenantName name, string logoUrl)
    {
        if (logoUrl is null)
        {
            return Errors.Tenant.NotFound;
        }

        if (name is null)
        {
            return Errors.Tenant.NameInvalid;
        }

        Name = name;
        LogoUrl = logoUrl;

        return this;
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

    public ErrorOr<Tenant> DeleteAccountId(AccountId id)
    {
        if (id is null)
        {
            return Errors.Account.NotFound;
        }

        if (!_accountIds.Contains(id))
        {
            return Errors.Account.NotFound;
        }

        _accountIds.Remove(id);

        return this;
    }

    public ErrorOr<Tenant> AddTasqId(TasqId id)
    {
        if (id is null)
        {
            return Errors.Tasq.NotFound;
        }

        if (_tasqIds.Contains(id))
        {
            return Errors.Tasq.AlreadyExists;
        }

        _tasqIds.Add(id);

        return this;
    }
}