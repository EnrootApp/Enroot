namespace Enroot.Application.Account.Common;

public record AccountModel(
    Guid Id,
    string? AvatarUrl,
    string Name,
    DateTime CreatedOn,
    string Email,
    int Role);