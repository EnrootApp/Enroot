using FluentValidation;

namespace Enroot.Application.Authorization.HasPermission;

public class HasPermissionQueryValidator : AbstractValidator<HasPermissionQuery>
{
    public HasPermissionQueryValidator()
    {
        RuleFor(c => c.Id)
           .NotEmpty();
        RuleFor(c => c.Permission)
           .NotEmpty();
    }
}