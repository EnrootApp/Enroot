using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tenant
    {
        public static Error NameDuplicate =>
            Error.Conflict(code: "Tenant.NameDuplicate");
        public static Error NameInvalid =>
            Error.Validation(code: "Tenant.NameInvalid");
    }
}