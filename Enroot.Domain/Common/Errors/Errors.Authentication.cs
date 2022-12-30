using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    // TODO: Localization
    public static class Authentication
    {
        public static Error CredentialsInvalid => Error.Validation(code: "Authentication.CredentialsInvalid", description: "Invalid credentials");
    }
}