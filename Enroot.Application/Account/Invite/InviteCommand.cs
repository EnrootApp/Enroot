using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Invite;

public record InviteCommand(string Email, Guid TenantId) : IRequest<ErrorOr<AccountResult>>;