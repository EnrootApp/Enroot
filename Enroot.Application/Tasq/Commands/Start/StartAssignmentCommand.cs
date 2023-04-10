using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Start;

public record StartAssignmentCommand(Guid AssigneeId, Guid AssignmentId) : IRequest<ErrorOr<TasqResult>>;
