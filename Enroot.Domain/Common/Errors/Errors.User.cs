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
           Error.Conflict(code: "User.NotFoundById");
        public static Error NotRegistered =>
           Error.Unexpected(code: "User.NotRegistered");
    }
}