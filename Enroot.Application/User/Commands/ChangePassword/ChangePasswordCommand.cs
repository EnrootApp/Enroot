using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : IRequest<ErrorOr<UserResult>>;