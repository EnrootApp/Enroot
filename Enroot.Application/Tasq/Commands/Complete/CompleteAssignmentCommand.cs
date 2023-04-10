using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Complete;

public record CompleteAssignmentCommand(
    Guid AssigneeId,
    Guid AssignmentId,
    IEnumerable<CreateAttachmentModel> Attachments) : IRequest<ErrorOr<TasqResult>>;
