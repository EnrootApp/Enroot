using Enroot.Application.Account.Common;

namespace Enroot.Application.Tasq.Queries.GetTasqs;

public record TasqResult(
    int Key,
    AccountModel Creator,
    AccountModel Assignee,
    string Title,
    bool IsCompleted,
    DateTime CreatedOn);

public record GetTasqsResult(IEnumerable<TasqResult> Tasqs, int TotalAmount);