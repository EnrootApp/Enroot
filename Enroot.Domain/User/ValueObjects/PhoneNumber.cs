using System.Text.RegularExpressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Domain.User.ValueObjects;

public sealed partial class PhoneNumber : ValueObject
{
    [GeneratedRegex(@"^\\S+@\\S+\\.\\S+$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex RegexValidator();

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException($"'{nameof(phoneNumber)}' cannot be null or whitespace.", nameof(phoneNumber));
        }

        if (!RegexValidator().IsMatch(phoneNumber))
        {
            throw new ArgumentException($"'{nameof(phoneNumber)}' is not a valid phone number.", nameof(phoneNumber));
        }

        return new(phoneNumber);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}