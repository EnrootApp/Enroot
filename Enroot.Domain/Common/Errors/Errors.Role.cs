using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Role
    {
        public static Error NotFoundById =>
            Error.NotFound(code: "Role.NotFoundById");
        public static Error InvalidName =>
            Error.Validation(code: "Role.InvalidName");
        public static Error PermissionExists =>
            Error.Conflict(code: "Role.PermissionExists");
    }
}