using FluentValidation;

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
                .WithErrorCode("Validation.EmailInvalid");
            RuleFor(c => c.Password).
                NotEmpty()
                .WithErrorCode("Validation.NotEmpty")
                .MinimumLength(6)
                .WithErrorCode("Validation.PasswordInvalid")
                .Matches("[a-z]")
                .WithErrorCode("Validation.PasswordInvalid");
        }
    }
}
