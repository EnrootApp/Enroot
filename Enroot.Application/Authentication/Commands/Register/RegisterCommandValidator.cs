using FluentValidation;

namespace Enroot.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress().WithErrorCode("Validation.EmailInvalid");
            RuleFor(c => c.Username).NotEmpty().MinimumLength(2).WithErrorCode("Validation.UsernameInvalid");
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6).WithErrorCode("Validation.PasswordInvalid");
        }
    }
}
