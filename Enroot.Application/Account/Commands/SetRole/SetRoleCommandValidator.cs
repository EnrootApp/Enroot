using FluentValidation;

namespace Enroot.Application.Account.Commands.SetRole;

public class SetRoleCommandValidator : AbstractValidator<SetRoleCommand>
{
    public SetRoleCommandValidator()
    {
        RuleFor(c => c.RoleId)
           .NotEmpty();
        RuleFor(c => c.AccountId)
           .NotEmpty();
    }
}