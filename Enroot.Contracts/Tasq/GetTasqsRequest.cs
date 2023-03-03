namespace Enroot.Contracts.Tasq;

public record GetTasqsRequest(string? Title, Guid? CreatorId, int[]? Statuses, int Skip = 0, int Take = 10);