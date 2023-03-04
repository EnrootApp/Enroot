using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Attachment
    {
        private const string _code = "Attachment";

        public static Error UrlInvalid =>
            Error.Validation(_code, "UrlInvalid");
        public static Error NameInvalid =>
            Error.Validation(_code, "NameInvalid");
        public static Error UploadFail =>
            Error.Unexpected(_code, "UploadFail");
    }
}