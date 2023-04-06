namespace Enroot.Contracts.Tasq;

public record GetTasqsRequest(
    Guid TenantId,
    string? Title,
    Guid? CreatorId,
    bool? IsCompleted,
    bool? IsAssigned,
    int Skip = 0,
    int Take = 10);