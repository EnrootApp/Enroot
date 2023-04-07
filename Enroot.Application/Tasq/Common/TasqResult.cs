using Enroot.Application.Account.Common;

namespace Enroot.Application.Tasq.Common;

public record TasqResult(
    Guid Id,
    DateTime CreatedOn,
    AccountModel Creator,
    string Title,
    string? Description,
    IEnumerable<AssignmentResult>? Assignments);