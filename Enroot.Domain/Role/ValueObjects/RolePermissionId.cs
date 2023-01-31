using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.Enums;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RolePermissionId : ValueObject
{
    public PermissionEnum Value { get; }

    private RolePermissionId(PermissionEnum value)
    {
        Value = value;
    }

    public static RolePermissionId Create(PermissionEnum id)
    {
        return new RolePermissionId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}