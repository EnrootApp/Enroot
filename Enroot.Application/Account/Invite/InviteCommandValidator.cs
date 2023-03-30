using FluentValidation;

namespace Enroot.Application.Account.Invite;

public class InviteCommandValidator : AbstractValidator<InviteCommand>
{
    public InviteCommandValidator()
    {
        RuleFor(c => c.Email)
           .NotEmpty();
        RuleFor(c => c.TenantId)
          .NotEmpty();
    }
}
