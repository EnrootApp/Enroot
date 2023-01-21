using Enroot.Application.Authentication.Common;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(query.Email);

        if (user is null)
        {
            return Errors.Authentication.CredentialsInvalid;
        }

        var isPasswordWrong = !await _userManager.CheckPasswordAsync(user, query.Password);

        if (isPasswordWrong)
        {
            return Errors.Authentication.CredentialsInvalid;
        }

        var claims = await _userManager.GetClaimsAsync(user);

        var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, claims);

        return new AuthenticationResult(accessToken);
    }
}