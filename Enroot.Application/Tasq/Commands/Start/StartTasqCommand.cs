using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Start;

public record StartTasqCommand(Guid AssigneeId, Guid TasqId) : IRequest<ErrorOr<TasqResult>>;
