using System.Text.RegularExpressions;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.User.ValueObjects;

public sealed partial class PhoneNumber : ValueObject
{
    [GeneratedRegex(@"^(29|25|44|33)(\d{3})(\d{2})(\d{2})$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex RegexValidator();

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static ErrorOr<PhoneNumber> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Errors.User.PhoneInvalid;
        }

        if (!RegexValidator().IsMatch(phoneNumber))
        {
            return Errors.User.PhoneInvalid;
        }

        return new PhoneNumber(phoneNumber);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}