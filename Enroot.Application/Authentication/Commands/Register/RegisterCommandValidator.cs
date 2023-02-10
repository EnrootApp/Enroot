using FluentValidation;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithErrorCode("Validation.NotEmpty")
                .EmailAddress()
                .WithErrorCode(Errors.User.EmailInvalid.Code);
            RuleFor(c => c.Password).
                NotEmpty()
                .WithErrorCode("Validation.NotEmpty")
                .MinimumLength(6)
                .WithErrorCode(Errors.User.PasswordInvalid.Code)
                .Matches("[a-z]")
                .WithErrorCode(Errors.User.PasswordInvalid.Code);
        }
    }
}
