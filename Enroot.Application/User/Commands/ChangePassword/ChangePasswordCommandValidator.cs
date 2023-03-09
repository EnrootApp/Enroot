using FluentValidation;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.User.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.OldPassword)
               .NotEmpty()
               .MinimumLength(6)
               .WithErrorCode(Errors.User.PasswordInvalid.Code)
               .WithMessage(Errors.User.PasswordInvalid.Description)
               .Matches("[a-z]")
               .WithErrorCode(Errors.User.PasswordInvalid.Code)
               .WithMessage(Errors.User.PasswordInvalid.Description);
        RuleFor(c => c.OldPassword)
           .NotEmpty();
    }
}