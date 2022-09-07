using Enroot.Application.Authentication.Common;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Domain.Common.Authorization;
using Enroot.Domain.Common.Enums;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is not null)
        {
            return Errors.User.EmailDuplicate;
        }

        user = await _userManager.FindByNameAsync(command.Username);

        if (user is not null)
        {
            return Errors.User.UsernameDuplicate;
        }

        user = new User
        {
            Email = command.Email,
            UserName = command.Username,
            RoleId = (int)EnrootRoles.User,
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            // TODO: send error if not created
        }

        var claims = await _userManager.GetClaimsAsync(user);

        var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, claims);

        return new AuthenticationResult(accessToken);
    }
}