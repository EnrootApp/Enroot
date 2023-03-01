namespace Enroot.Contracts.Tasq;

public record TasqResponse(
    Guid CreatorId,
    Guid TenantId,
    string? Description,
    IEnumerable<AssignmentResponse> Assignments);