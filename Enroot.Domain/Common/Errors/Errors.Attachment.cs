using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Attachment
    {
        private const string _code = "Attachment";

        public static Error UrlInvalid =>
            Error.Conflict(_code, "UrlInvalid");
        public static Error NameInvalid =>
            Error.Conflict(_code, "NameInvalid");
    }
}