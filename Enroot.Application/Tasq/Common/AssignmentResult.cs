namespace Enroot.Application.Tasq.Common;

public record AssignmentResult(Guid AssigneeId, Guid AssignerId, int Status);