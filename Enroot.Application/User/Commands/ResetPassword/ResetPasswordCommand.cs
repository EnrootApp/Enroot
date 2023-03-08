using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Commands.ResetPassword;

public record ResetPasswordCommand(string Email, string Code, string NewPassword) : IRequest<ErrorOr<UserResult>>;