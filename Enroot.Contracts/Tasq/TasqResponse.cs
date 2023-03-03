namespace Enroot.Contracts.Tasq;

public record TasqResponse(
    Guid CreatorId,
    string Title,
    string? Description,
    IEnumerable<AssignmentResponse> Assignments);