using Enroot.Domain.Common.Models;

namespace Enroot.Domain.User.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid id) => new(id);

    public static UserId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}