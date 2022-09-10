﻿using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error CredentialsInvalid => Error.Validation(code: "Authentication.CredentialsInvalid");
    }
}