using Enroot.Application.Authentication.Common;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRepository<User, UserId> _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher, IRepository<User, UserId> userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(u => u.Email! == Email.Create(command.Email));

        if (user is not null)
        {
            return Errors.User.EmailDuplicate;
        }

        var passwordHash = _passwordHasher.HashPassword(null!, command.Password);

        var emailResult = Email.Create(command.Email);

        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        var createUserResult = User.CreateByEmail(emailResult.Value, passwordHash);

        if (createUserResult.IsError)
        {
            return createUserResult.Errors;
        }

        var persistedUser = await _userRepository.CreateAsync(createUserResult.Value);

        var token = _jwtTokenGenerator.GenerateToken(persistedUser);

        return new AuthenticationResult(token);
    }
}