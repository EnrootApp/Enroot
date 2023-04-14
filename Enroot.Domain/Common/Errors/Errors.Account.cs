using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        private const string _code = "Account";

        public static Error NotFound =>
           Error.NotFound(_code, "NotFound");
        public static Error NotDeleted =>
           Error.NotFound(_code, "NorDeleted");
        public static Error AlreadyExists =>
           Error.Conflict(_code, "AlreadyExists");
    }
}