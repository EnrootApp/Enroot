using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.Enums;
using ErrorOr;

namespace Enroot.Domain.Permission.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public PermissionEnum Value { get; }

    private PermissionId(PermissionEnum value)
    {
        Value = value;
    }

    public static ErrorOr<PermissionId> Create(PermissionEnum id)
    {
        if (!Enum.IsDefined(typeof(PermissionEnum), id))
        {
            return Common.Errors.Errors.Permission.NotFound;
        }

        return new PermissionId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}