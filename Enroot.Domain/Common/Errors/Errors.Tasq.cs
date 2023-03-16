using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tasq
    {
        private const string _code = "Tasq";
        public static Error TitleInvalid =>
            Error.Conflict(_code, "TitleInvalid");
        public static Error AlreadyCompleted =>
            Error.Conflict(_code, "AlreadyCompleted");
        public static Error AlreadyAssigned =>
            Error.Conflict(_code, "AlreadyAssigned");
        public static Error NotFound =>
           Error.NotFound(_code, "NotFound");
    }
}