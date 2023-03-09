using FluentValidation;

namespace Enroot.Application.User.Queries.ResetPasswordEmail;

public class ResetPasswordEmailQueryValidator : AbstractValidator<ResetPasswordEmailQuery>
{
    public ResetPasswordEmailQueryValidator()
    {
        RuleFor(c => c.Email)
           .NotEmpty();
    }
}