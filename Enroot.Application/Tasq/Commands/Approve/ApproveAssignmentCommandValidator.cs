using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Approve;

public class ApproveAssignmentCommandValidator : AbstractValidator<ApproveAssignmentCommand>
{
    public ApproveAssignmentCommandValidator()
    {
        RuleFor(c => c.AssignmentId)
           .NotEmpty();
        RuleFor(c => c.ReviewerId)
           .NotEmpty();
    }
}