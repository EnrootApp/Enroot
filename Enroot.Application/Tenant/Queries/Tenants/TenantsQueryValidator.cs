using FluentValidation;

namespace Enroot.Application.Tenant.Queries.Tenants;

public class TenantsQueryValidator : AbstractValidator<TenantsQuery>
{
    public TenantsQueryValidator()
    {
        RuleFor(c => c.IsParticipate)
           .NotNull();
        RuleFor(c => c.UserId)
           .NotEmpty();
        RuleFor(c => c.Skip)
           .NotNull()
           .GreaterThanOrEqualTo(0);
        RuleFor(c => c.Take)
           .NotNull()
           .GreaterThanOrEqualTo(1);
    }
}