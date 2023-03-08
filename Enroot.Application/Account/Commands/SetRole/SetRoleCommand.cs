using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Commands.SetRole;

public record SetRoleCommand(Guid AccountId, int RoleId) : IRequest<ErrorOr<AccountResult>>;