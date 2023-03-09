using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.Enums;
using ErrorOr;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RolePermissionId : ValueObject
{
    public PermissionEnum Value { get; }

    private RolePermissionId(PermissionEnum value)
    {
        Value = value;
    }

    public static ErrorOr<RolePermissionId> Create(PermissionEnum id)
    {
        if (!Enum.IsDefined(typeof(PermissionEnum), id))
        {
            return Common.Errors.Errors.Permission.NotFound;
        }

        return new RolePermissionId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}