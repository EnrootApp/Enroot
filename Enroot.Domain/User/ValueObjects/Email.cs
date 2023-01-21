using Enroot.Domain.Common.Models;

namespace Enroot.Domain.User.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
        }

        // TODO: Email regex validation

        return new(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}