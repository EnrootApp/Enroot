using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Role
    {
        private const string _code = "Role";

        public static Error NotFound =>
            Error.NotFound(_code, "NotFound");
        public static Error InvalidName =>
            Error.Validation(_code, "InvalidName");
        public static Error PermissionExists =>
            Error.Conflict(_code, "PermissionExists");
    }
}