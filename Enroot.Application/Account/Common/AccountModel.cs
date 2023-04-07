namespace Enroot.Application.Account.Common;

public record AccountModel(
    Guid Id,
    string? AvatarUrl,
    string Name);