namespace Enroot.Application.User.Common;

public record UserResult(
    string Email,
    string Role,
    string LastName,
    string FirstName,
    string AvatarUrl,
    IEnumerable<Guid> AccountIds);