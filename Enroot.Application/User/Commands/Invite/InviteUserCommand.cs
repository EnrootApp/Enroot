using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Commands.Invite;

public record InviteUserCommand(string Email, Guid TenantId) : IRequest<ErrorOr<AccountResult>>;