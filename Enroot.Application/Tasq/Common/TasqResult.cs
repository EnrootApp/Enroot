namespace Enroot.Application.Tasq.Common;

public record TasqResult(
    Guid TasqId,
    Guid CreatorId,
    string? Description,
    string Title,
    IEnumerable<AssignmentResult> Assignments);