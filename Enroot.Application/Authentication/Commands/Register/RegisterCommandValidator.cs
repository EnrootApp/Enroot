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
                .WithErrorCode("Validation")
                .WithMessage("NotEmpty")
                .EmailAddress()
                .WithErrorCode(Errors.User.EmailInvalid.Code)
                .WithMessage(Errors.User.EmailInvalid.Description);
            RuleFor(c => c.Password)
                .NotEmpty()
                .WithErrorCode("Validation")
                .WithMessage("NotEmpty")
                .MinimumLength(6)
                .WithErrorCode(Errors.User.PasswordInvalid.Code)
                .WithMessage(Errors.User.PasswordInvalid.Description)
                .Matches("[a-z]")
                .WithErrorCode(Errors.User.PasswordInvalid.Code)
                .WithMessage(Errors.User.PasswordInvalid.Description);
        }
    }
}
