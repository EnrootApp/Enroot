using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Complete;

public record CompleteAssignmentCommand(
    Guid AssigneeId,
    Guid AssignmentId,
    string? FeedbackMessage,
    IEnumerable<CreateAttachmentModel> Attachments) : IRequest<ErrorOr<TasqResult>>;
