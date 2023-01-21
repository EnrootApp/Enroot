using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RoleId : ValueObject
{
    public int Value { get; }

    private RoleId(int value)
    {
        Value = value;
    }

    public static RoleId Create(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be less than 1.", nameof(id));
        }

        return new RoleId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}