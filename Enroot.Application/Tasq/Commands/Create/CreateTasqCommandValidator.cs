
using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Create;

public class CreateTasqCommandValidator : AbstractValidator<CreateTasqCommand>
{
    public CreateTasqCommandValidator()
    {
        RuleFor(c => c.Description).MaximumLength(1000);
        RuleFor(c => c.CreatorId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
    }
}
