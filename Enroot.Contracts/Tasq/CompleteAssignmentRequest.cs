namespace Enroot.Contracts.Tasq;

public record CompleteAssignmentRequest(Guid TasqId, IEnumerable<AttachmentRequest> Attachments);