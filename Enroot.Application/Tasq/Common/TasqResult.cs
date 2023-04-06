namespace Enroot.Application.Tasq.Common;

public record TasqResult(
    Guid TasqId,
    Guid CreatorId,
    string CreatorName,
    string Title,
    string? Description,
    IEnumerable<AssignmentResult>? Assignments);