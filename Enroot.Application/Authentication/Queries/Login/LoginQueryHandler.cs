using Enroot.Application.Authentication.Common;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.User;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRepository<User, UserId> _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IRepository<User, UserId> userRepository, IPasswordHasher<User> passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(user => user.Email! == Email.Create(query.Email));

        if (user is null)
        {
            return Errors.Authentication.CredentialsInvalid;
        }

        var isPasswordWrong = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.Password) == PasswordVerificationResult.Failed;

        if (isPasswordWrong)
        {
            return Errors.Authentication.CredentialsInvalid;
        }

        var accessToken = _jwtTokenGenerator.GenerateToken(user.Id);

        return new AuthenticationResult(accessToken);
    }
}