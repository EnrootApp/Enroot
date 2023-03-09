
using FluentValidation;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.Tasq.Commands.Reject;

public class RejectAssignmentCommandValidator : AbstractValidator<RejectAssignmentCommand>
{
    public RejectAssignmentCommandValidator()
    {
        RuleFor(c => c.AssignmentId).NotEmpty();
        RuleFor(c => c.FeedbackMessage).MaximumLength(255);
        RuleFor(c => c.ReviewerId).NotEmpty();
    }
}
