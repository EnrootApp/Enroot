using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        private const string _code = "User";

        public static Error CredentialsInvalid =>
            Error.Conflict(_code, "CredentialsInvalid");
        public static Error EmailDuplicate =>
            Error.Conflict(_code, "EmailDuplicate");
        public static Error NotFound =>
           Error.NotFound(_code, "NotFound");
        public static Error EmailInvalid =>
            Error.Validation(_code, "EmailInvalid");
        public static Error PhoneInvalid =>
            Error.Validation(_code, "PhoneInvalid");
        public static Error PasswordInvalid =>
            Error.Validation(_code, "PasswordInvalid");
        public static Error AccountExists =>
            Error.Conflict(_code, "AccountExists");
    }
}