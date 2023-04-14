using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Commands.Restore;

public record RestoreAccountCommand(Guid AccountId) : IRequest<ErrorOr<AccountResult>>;