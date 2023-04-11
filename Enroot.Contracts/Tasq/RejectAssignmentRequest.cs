namespace Enroot.Contracts.Tasq;

public record RejectAssignmentRequest(Guid AssignmentId, string FeedbackMessage);