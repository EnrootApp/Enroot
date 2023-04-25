namespace Enroot.Contracts.Tasq;

public record CompleteAssignmentRequest(
    Guid AssignmentId,
    string? FeedbackMessage,
    IEnumerable<AttachmentRequest> Attachments);