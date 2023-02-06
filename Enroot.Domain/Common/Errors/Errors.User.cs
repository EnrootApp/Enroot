using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error EmailDuplicate =>
            Error.Conflict(code: "User.EmailDuplicate");
        public static Error UsernameDuplicate =>
            Error.Conflict(code: "User.UsernameDuplicate");
        public static Error NotFoundById =>
           Error.NotFound(code: "User.NotFoundById");
        public static Error NotRegistered =>
           Error.Unexpected(code: "User.NotRegistered");
        public static Error EmailInvalid =>
            Error.Validation(code: "User.EmailInvalid");
        public static Error PhoneInvalid =>
            Error.Validation(code: "User.PhoneInvalid");
        public static Error PasswordInvalid =>
            Error.Validation(code: "User.PasswordInvalid");
        public static Error AccountExists =>
            Error.Validation(code: "User.AccountExists");
    }
}