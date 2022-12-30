using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities;

public class User : IdentityUser<int>
{
    public virtual List<UserLeaf>? UserLeaves { get; }
    public virtual UserPreferences? UserPreferences { get; }
}