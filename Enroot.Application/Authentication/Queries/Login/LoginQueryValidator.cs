using FluentValidation;
using Enroot.Domain.Common.Errors;

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
                .WithErrorCode(Errors.User.EmailInvalid.Description);
            RuleFor(c => c.Password)
                .NotEmpty()
                .WithErrorCode("Validation.NotEmpty");
        }
    }
}
