namespace Enroot.Contracts.Tasq;

public record TasqResponse(
    Guid TasqId,
    Guid CreatorId,
    string CreatorName,
    string Title,
    string? Description,
    IEnumerable<AssignmentResponse> Assignments
    );