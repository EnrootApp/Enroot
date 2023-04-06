using Enroot.Application.Tasq.Common;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public record TasqResult(
    int Key,
    AccountModel Creator,
    string Title,
    bool IsAssigned,
    bool IsCompleted);

public record GetTasqsResult(IEnumerable<TasqResult> Tasqs, int TotalAmount);