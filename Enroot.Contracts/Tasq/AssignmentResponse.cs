namespace Enroot.Contracts.Tasq;

public record AssignmentResponse(
    Guid AssigneeId,
    Guid AssignerId,
    string AssigneeName,
    string AssignerName,
    int Status,
    IEnumerable<AttachmentResponse> Attachments);