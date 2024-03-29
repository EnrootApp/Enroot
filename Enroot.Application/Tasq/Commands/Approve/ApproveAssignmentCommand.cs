using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Approve;

public record ApproveAssignmentCommand(
    Guid ReviewerId,
    Guid AssignmentId,
    string? FeedbackMessage) : IRequest<ErrorOr<TasqResult>>;
