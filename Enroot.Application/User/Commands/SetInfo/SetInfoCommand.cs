using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Commands.SetInfo;

public record SetInfoCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string AvatarUrl) : IRequest<ErrorOr<UserResult>>;