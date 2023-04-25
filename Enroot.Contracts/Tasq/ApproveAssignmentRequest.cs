namespace Enroot.Contracts.Tasq;

public record ApproveAssignmentRequest(Guid AssignmentId, string? FeedbackMessage);