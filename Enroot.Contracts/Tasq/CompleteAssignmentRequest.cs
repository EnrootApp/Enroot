namespace Enroot.Contracts.Tasq;

public record CompleteAssignmentRequest(Guid AssignmentId, IEnumerable<AttachmentRequest> Attachments);