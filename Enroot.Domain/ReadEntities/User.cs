using Enroot.Domain.Common.Models;

namespace Enroot.Domain.ReadEntities;

public class UserRead : ReadEntity
{
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string Role { get; private set; } = default!;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? AvatarUrl { get; private set; }
    public ICollection<AccountRead> Accounts { get; private set; } = default!;
}