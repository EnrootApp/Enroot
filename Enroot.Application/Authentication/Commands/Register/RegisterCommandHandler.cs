using Enroot.Application.Authentication.Common;
using Enroot.Application.Common.Interfaces.Authentication;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.User.ValueObjects;
using UserEntity = Enroot.Domain.User.User;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly IRepository<UserEntity, UserId> _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher<UserEntity> passwordHasher,
        IRepository<UserEntity, UserId> userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var email = Email.Create(command.Email).Value;

        var user = await _userRepository.FindAsync(u => u.Email! == email, cancellationToken);

        if (user is not null)
        {
            return Errors.User.EmailDuplicate;
        }

        var passwordHash = _passwordHasher.HashPassword(null!, command.Password);

        var createUserResult = UserEntity.CreateByEmail(email, passwordHash);

        if (createUserResult.IsError)
        {
            return createUserResult.Errors;
        }

        var persistedUser = await _userRepository.CreateAsync(createUserResult.Value, cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(persistedUser);

        return new AuthenticationResult(token);
    }
}