using System.Text.RegularExpressions;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.User.ValueObjects;

public sealed partial class Name : ValueObject
{
    [GeneratedRegex(@"^[^()[\]{}*&^%$#@!\s]{1,50}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex NameValidator();

    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }

    public static ErrorOr<Name> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.User.EmailInvalid;
        }

        if (!NameValidator().IsMatch(email))
        {
            return Errors.User.EmailInvalid;
        }

        return new Name(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}