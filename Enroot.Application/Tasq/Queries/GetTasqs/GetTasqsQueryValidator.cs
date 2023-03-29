using FluentValidation;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public class GetTasqsQueryValidator : AbstractValidator<GetTasqsQuery>
{
    public GetTasqsQueryValidator()
    {
        RuleFor(c => c.TenantId)
           .NotEmpty();
        RuleFor(c => c.Skip)
           .NotNull()
           .GreaterThanOrEqualTo(0);
        RuleFor(c => c.Take)
           .NotNull()
           .GreaterThanOrEqualTo(1);
        RuleFor(c => c.Title).MaximumLength(100);
    }
}