using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Assign;

public record AssignTasqCommand(Guid AssignerId, Guid AssigneeId, Guid TasqId) : IRequest<ErrorOr<TasqResult>>;