using FluentValidation;

namespace Enroot.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithErrorCode("Validation.NotEmpty")
                .EmailAddress()
                .WithErrorCode("Validation.EmailInvalid");
            RuleFor(c => c.Password)
                .NotEmpty()
                .WithErrorCode("Validation.NotEmpty");
        }
    }
}
