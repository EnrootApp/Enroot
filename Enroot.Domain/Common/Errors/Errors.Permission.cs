using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Permission
    {
        private const string _code = "Permission";

        public static Error NotFound =>
            Error.NotFound(_code, "NotFound");
        public static Error InvalidDescription =>
            Error.Validation(_code, "InvalidDescription");
    }
}