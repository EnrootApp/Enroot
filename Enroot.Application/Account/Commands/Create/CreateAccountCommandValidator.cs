using FluentValidation;

namespace Enroot.Application.Account.Commands.Create;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(c => c.RoleId)
           .NotEmpty();
        RuleFor(c => c.UserId)
           .NotEmpty();
        RuleFor(c => c.TenantId)
           .NotEmpty();
    }
}