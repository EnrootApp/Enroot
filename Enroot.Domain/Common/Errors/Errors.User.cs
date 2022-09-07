using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    // TODO: Localization
    public static class User
    {
        public static Error EmailDuplicate =>
            Error.Conflict(code: "User.EmailDuplicate", description: "Email is already in use");
        public static Error UsernameDuplicate =>
            Error.Conflict(code: "User.UsernameDuplicate", description: "UserName is already in use");
        public static Error NotFoundById =>
           Error.Conflict(code: "User.NotFoundById", description: "No user found with id specified");
    }
}