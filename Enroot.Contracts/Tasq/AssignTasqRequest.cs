namespace Enroot.Contracts.Tasq;

public record AssignTasqRequest(Guid TasqId, Guid AssigneeId);