using FluentValidation;

namespace Enroot.Application.User.Commands.Invite;

public class InviteUserCommandValidator : AbstractValidator<InviteUserCommand>
{
    public InviteUserCommandValidator()
    {
        RuleFor(c => c.Email)
           .NotEmpty();
        RuleFor(c => c.TenantId)
          .NotEmpty();
    }
}
