using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Reject;

public record RejectAssignmentCommand(
    Guid ReviewerId,
    Guid AssignmentId,
    string FeedbackMessage) : IRequest<ErrorOr<TasqResult>>;
