using FluentValidation;

namespace Enroot.Application.User.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(c => c.Email)
           .NotEmpty();
        RuleFor(c => c.NewPassword)
          .NotEmpty();
    }
}
