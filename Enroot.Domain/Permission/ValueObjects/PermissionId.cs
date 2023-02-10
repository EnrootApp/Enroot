using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.Enums;

namespace Enroot.Domain.Permission.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public PermissionEnum Value { get; }

    private PermissionId(PermissionEnum value)
    {
        Value = value;
    }

    public static PermissionId Create(PermissionEnum id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}