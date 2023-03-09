using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Complete;

public class CompleteAssignmentCommandValidator : AbstractValidator<CompleteAssignmentCommand>
{
    public CompleteAssignmentCommandValidator()
    {
        RuleFor(c => c.AssigneeId)
           .NotEmpty();
        RuleFor(c => c.TasqId)
           .NotEmpty();
        RuleFor(c => c.Attachments)
            .NotNull();
    }
}