using Enroot.Domain.Common.Models;

namespace Enroot.Domain.User.ValueObjects;

public sealed class Username : ValueObject
{
    public string Value { get; }

    private Username(string value)
    {
        Value = value;
    }

    public static Username Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        return new(name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}