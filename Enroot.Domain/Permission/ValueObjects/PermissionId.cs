using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Permission.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public int Value { get; }

    private PermissionId(int value)
    {
        Value = value;
    }

    public static PermissionId Create(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be less than 1.", nameof(id));
        }

        return new PermissionId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}