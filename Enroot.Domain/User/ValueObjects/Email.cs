using System.Text.RegularExpressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Domain.User.ValueObjects;

public sealed partial class Email : ValueObject
{
    [GeneratedRegex(@"^\S+@\S+\.\S+$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex EmailValidator();

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

        if (!EmailValidator().IsMatch(email))
        {
            throw new ArgumentException($"'{nameof(email)}' is not a valid email.", nameof(email));
        }

        return new(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}