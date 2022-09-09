using FluentValidation;

namespace Enroot.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Username).NotEmpty().MinimumLength(2);
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6);
        }
    }
}
