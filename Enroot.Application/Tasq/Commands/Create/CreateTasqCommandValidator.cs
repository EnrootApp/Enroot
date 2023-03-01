
using FluentValidation;
using Enroot.Domain.Common.Errors;

namespace Enroot.Application.Tasq.Commands.Create;

public class CreateTasqCommandValidator : AbstractValidator<CreateTasqCommand>
{
    public CreateTasqCommandValidator()
    {
        RuleFor(c => c.Description).MaximumLength(1000);
    }
}