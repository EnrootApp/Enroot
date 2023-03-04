using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Complete;

public record CompleteAssignmentCommand(
    Guid AssigneeId,
    Guid TasqId,
    IEnumerable<CreateAttachmentModel> Attachments) : IRequest<ErrorOr<TasqResult>>;
