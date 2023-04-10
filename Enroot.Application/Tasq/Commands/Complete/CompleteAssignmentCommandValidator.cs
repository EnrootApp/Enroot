using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Complete;

public class CompleteAssignmentCommandValidator : AbstractValidator<CompleteAssignmentCommand>
{
    public CompleteAssignmentCommandValidator()
    {
        RuleFor(c => c.AssigneeId)
           .NotEmpty();
        RuleFor(c => c.AssignmentId)
           .NotEmpty();
        RuleFor(c => c.Attachments)
            .NotNull()
            .Must(c => c.Count() <= 32)
            .ForEach(a => a.NotNull())
            .ForEach(a => a.ChildRules(a => a.RuleFor(a => a.Name).MaximumLength(64)))
            .ForEach(a => a.ChildRules(a => a.RuleFor(a => a.Url).NotEmpty()));
    }
}