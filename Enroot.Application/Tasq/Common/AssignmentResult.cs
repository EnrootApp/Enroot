namespace Enroot.Application.Tasq.Common;

public record AssignmentResult(
    Guid AssigneeId,
    string AssigneeName,
    Guid AssignerId,
    string AssignerName,
    int Status,
    IEnumerable<AttachmentModel> Attachments);