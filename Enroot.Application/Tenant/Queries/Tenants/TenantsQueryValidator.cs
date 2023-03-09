using FluentValidation;

namespace Enroot.Application.Tenant.Queries.Tenants;

public class TenantsQueryValidator : AbstractValidator<TenantsQuery>
{
    public TenantsQueryValidator()
    {
        RuleFor(c => c.IsParticipate)
           .NotEmpty();
        RuleFor(c => c.UserId)
           .NotEmpty();
        RuleFor(c => c.Offset)
           .NotEmpty();
        RuleFor(c => c.Take)
           .NotEmpty();
    }
}