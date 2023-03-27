using FluentValidation;

namespace Enroot.Application.User.Commands.SetInfo;

public class SetInfoCommandValidator : AbstractValidator<SetInfoCommand>
{
    public SetInfoCommandValidator()
    {
        RuleFor(c => c.FirstName)
           .NotEmpty()
           .MaximumLength(50);
        RuleFor(c => c.LastName)
          .NotEmpty()
           .MaximumLength(50);
        RuleFor(c => c.AvatarUrl)
          .NotNull();
    }
}
