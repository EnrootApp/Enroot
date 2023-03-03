namespace Enroot.Application.Tasq.Common;

public record TasqResult(
    Guid Id,
    Guid CreatorId,
    string? Description,
    string Title,
    IEnumerable<AssignmentResult> Assignments);