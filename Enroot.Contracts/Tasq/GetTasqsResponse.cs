namespace Enroot.Application.Tasq.Queries.GetTasqs;

public record GetTasqsResult(
    int Key,
    string CreatorId,
    string CreatorName,
    string CreatorAvatarUrl,
    string Title,
    bool IsAssigned,
    bool IsCompleted);