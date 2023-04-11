using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;
using Enroot.Domain.Common.Errors;
using UserEntity = Enroot.Domain.User.User;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Mapster;

namespace Enroot.Application.User.Commands.SetInfo;

public class SetInfoCommandHandler : IRequestHandler<SetInfoCommand, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;

    public SetInfoCommandHandler(IRepository<UserEntity, UserId> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResult>> Handle(SetInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.UserId), cancellationToken: cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var firstName = Name.Create(request.FirstName);

        if (firstName.IsError)
        {
            return Errors.User.NameInvalid;
        }

        var lastName = Name.Create(request.LastName);

        if (lastName.IsError)
        {
            return Errors.User.NameInvalid;
        }

        user.UpdateInfo(firstName.Value, lastName.Value, request.AvatarUrl);
        await _userRepository.UpdateAsync(user);

        return user.Adapt<UserResult>();
    }
}
