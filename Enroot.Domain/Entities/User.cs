using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities;

public class User : IdentityUser<int>
{
    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}