using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tenant
    {
        private const string _code = "Tenant";

        public static Error NameDuplicate =>
            Error.Conflict(_code, "NameDuplicate");
        public static Error NameInvalid =>
            Error.Validation(_code, "NameInvalid");
        public static Error NotFound =>
            Error.NotFound(_code, "NotFound");
        public static Error AccountExists =>
           Error.Conflict(_code, "AccountExists");
    }
}