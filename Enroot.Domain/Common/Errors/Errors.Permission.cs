using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Permission
    {
        public static Error NotFoundById =>
            Error.NotFound(code: "Permission.NotFoundById");
        public static Error InvalidDescription =>
            Error.Validation(code: "Permission.InvalidDescription");
    }
}