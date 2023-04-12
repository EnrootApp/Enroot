using Enroot.Application.Account.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Commands.Delete;

public record DeleteAccountCommand(Guid AccountId, Guid RemoverId) : IRequest<ErrorOr<AccountResult>>;