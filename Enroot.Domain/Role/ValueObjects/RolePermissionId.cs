using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Role.ValueObjects;

public sealed class RolePermissionId : ValueObject
{
    public int Value { get; }

    private RolePermissionId(int value)
    {
        Value = value;
    }

    public static RolePermissionId Create(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be less than 1.", nameof(id));
        }

        return new RolePermissionId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}