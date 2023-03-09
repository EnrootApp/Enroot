using FluentValidation;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public class GetTasqsQueryValidator : AbstractValidator<GetTasqsQuery>
{
    public GetTasqsQueryValidator()
    {
        RuleFor(c => c.TenantId)
           .NotEmpty();
        RuleFor(c => c.Skip)
           .NotEmpty();
        RuleFor(c => c.Take)
           .NotEmpty();
        RuleFor(c => c.Title).MaximumLength(100);
    }
}