using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Assign;

public class AssignTasqCommandValidator : AbstractValidator<AssignTasqCommand>
{
    public AssignTasqCommandValidator()
    {
        RuleFor(c => c.AssigneeId)
           .NotEmpty();
        RuleFor(c => c.AssignerId)
           .NotEmpty();
        RuleFor(c => c.TasqId)
           .NotEmpty();
    }
}