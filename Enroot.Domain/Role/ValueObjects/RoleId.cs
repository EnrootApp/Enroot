using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.Enums;
using ErrorOr;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RoleId : ValueObject
{
    public RoleEnum Value { get; }

    private RoleId(RoleEnum value)
    {
        Value = value;
    }

    public static ErrorOr<RoleId> Create(RoleEnum id)
    {
        if (!Enum.IsDefined(typeof(RoleEnum), id))
        {
            return Common.Errors.Errors.Role.NotFound;
        }

        return new RoleId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}