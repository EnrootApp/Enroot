using FluentValidation;

namespace Enroot.Application.Tenant.Commands.Update;

public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(c => c.TenantId)
            .NotEmpty();
    }
}