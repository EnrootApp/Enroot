using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Tenant.ValueObjects;

public sealed class TenantId : ValueObject
{
    public Guid Value { get; }

    private TenantId(Guid value)
    {
        Value = value;
    }

    public static TenantId Create(Guid id) => new(id);
    public static TenantId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}