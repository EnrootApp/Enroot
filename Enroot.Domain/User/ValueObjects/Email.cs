using System.Text.RegularExpressions;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;

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
            throw new DomainException(Errors.User.EmailInvalid);
        }

        if (!EmailValidator().IsMatch(email))
        {
            throw new DomainException(Errors.User.EmailInvalid);
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}