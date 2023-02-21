using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tasq
    {
        private const string _code = "Tasq";

        public static Error AttachmentUrlInvalid =>
            Error.Conflict(_code, "AttachmentUrlInvalid");
        public static Error AttachmentNameInvalid =>
            Error.Conflict(_code, "AttachmentNameInvalid");
        public static Error AlreadyCompleted =>
            Error.Conflict(_code, "AlreadyCompleted");
        public static Error NotOnReview =>
            Error.Conflict(_code, "NotOnReview");
        public static Error AlreadyAssigned =>
            Error.Conflict(_code, "AlreadyAssigned");
        public static Error NotFound =>
           Error.NotFound(_code, "NotFound");
        public static Error AssignmentNotFound =>
            Error.NotFound(_code, "AssignmentNotFound");
    }
}