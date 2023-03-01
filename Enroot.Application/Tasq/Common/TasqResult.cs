namespace Enroot.Application.Tasq.Common;

public record TasqResult(
    Guid CreatorId,
    Guid TenantId,
    string? Description,
    IEnumerable<AssignmentResult> Assignments);