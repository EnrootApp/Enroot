
using FluentValidation;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.Tasq.Commands.Start;

public class StartAssignmentCommandValidator : AbstractValidator<StartAssignmentCommand>
{
    public StartAssignmentCommandValidator()
    {
        RuleFor(c => c.AssigneeId).NotEmpty();
        RuleFor(c => c.TasqId).NotEmpty();
    }
}
