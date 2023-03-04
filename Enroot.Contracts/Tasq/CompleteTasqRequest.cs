namespace Enroot.Contracts.Tasq;

public record CompleteTasqRequest(Guid TasqId, IEnumerable<AttachmentRequest> Attachments);