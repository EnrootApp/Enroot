using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error NotFoundById =>
           Error.NotFound(code: "Account.NotFoundById");
    }
}