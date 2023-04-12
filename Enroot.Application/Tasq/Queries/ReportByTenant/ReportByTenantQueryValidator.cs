using FluentValidation;

namespace Enroot.Application.Tasq.Queries.ReportByTenant;

public class ReportByTenantQueryValidator : AbstractValidator<ReportByTenantQuery>
{
    public ReportByTenantQueryValidator()
    {
        RuleFor(c => c.TenantId)
           .NotEmpty();
        RuleFor(c => c.From)
           .NotEmpty();
        RuleFor(c => c.To)
          .NotEmpty()
          .GreaterThan(r => r.From)
          .Must((query, to) =>
            {
                var timeSpan = to - query.From;
                return timeSpan.TotalDays <= 31;
            });
    }
}