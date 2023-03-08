using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.User.Common;
using Enroot.Domain.User.ValueObjects;
using ErrorOr;
using MediatR;
using Enroot.Domain.Common.Errors;
using UserEntity = Enroot.Domain.User.User;
using Microsoft.AspNetCore.Identity;
using MapsterMapper;
using Mapster;

namespace Enroot.Application.User.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;

    public ChangePasswordCommandHandler(
        IRepository<UserEntity, UserId> userRepository,
        IPasswordHasher<UserEntity> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<UserResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var isPasswordWrong = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.OldPassword) == PasswordVerificationResult.Failed;

        if (isPasswordWrong)
        {
            return Errors.User.CredentialsInvalid;
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, command.NewPassword); ;

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserResult>();
    }
}
