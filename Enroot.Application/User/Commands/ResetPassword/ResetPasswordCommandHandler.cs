using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;
using Enroot.Domain.Common.Errors;
using UserEntity = Enroot.Domain.User.User;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Mapster;

namespace Enroot.Application.User.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;

    public ResetPasswordCommandHandler(
        IRepository<UserEntity, UserId> userRepository,
        IPasswordHasher<UserEntity> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<UserResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }

        var user = await _userRepository.FindAsync(u => u.Email! == email.Value, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, request.Code, user.PasswordHash);
        if (result != PasswordVerificationResult.Success)
        {
            return Errors.User.ResetPasswordCodeInvalid;
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);

        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserResult>();
    }
}
