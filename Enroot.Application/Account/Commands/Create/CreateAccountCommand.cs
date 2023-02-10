using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Commands.Create;

public record CreateAccountCommand(Guid UserId, Guid TenantId) : IRequest<ErrorOr<AccountResult>>;