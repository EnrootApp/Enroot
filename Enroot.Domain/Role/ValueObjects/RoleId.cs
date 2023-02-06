using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.Enums;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RoleId : ValueObject
{
    public RoleEnum Value { get; }

    private RoleId(RoleEnum value)
    {
        Value = value;
    }

    public static RoleId Create(RoleEnum id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}