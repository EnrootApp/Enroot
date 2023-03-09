using FluentValidation;

namespace Enroot.Application.Tenant.Commands.Create;

public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator()
    {
        RuleFor(c => c.Name)
           .NotEmpty();
    }
}