using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using System.Text.RegularExpressions;

namespace Enroot.Domain.Tenant.ValueObjects;

public sealed partial class TenantName : ValueObject
{
    [GeneratedRegex(@"^((?!-))[A-Za-z0-9][A-Za-z0-9-_]{2,61}[A-Za-z0-9]{0,1}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
    private static partial Regex NameValidator();
    public string Value { get; }

    private TenantName(string value)
    {
        Value = value;
    }

    public static ErrorOr<TenantName> Create(string name)
    {
        if (!NameValidator().IsMatch(name))
        {
            return Errors.Tenant.NameInvalid;
        }

        return new TenantName(name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}