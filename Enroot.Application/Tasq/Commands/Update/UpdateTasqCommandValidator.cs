
using FluentValidation;

namespace Enroot.Application.Tasq.Commands.Update;

public class UpdateTasqCommandValidator : AbstractValidator<UpdateTasqCommand>
{
    public UpdateTasqCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.Description).MaximumLength(1000);
    }
}
