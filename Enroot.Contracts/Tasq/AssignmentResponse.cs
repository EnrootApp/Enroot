namespace Enroot.Contracts.Tasq;

public record AssignmentResponse(Guid AssigneeId, Guid AssignerId, int Status);