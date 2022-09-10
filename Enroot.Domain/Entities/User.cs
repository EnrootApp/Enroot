using Enroot.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities;

public class User : IdentityUser<int>
{
    internal protected User () { }

    internal User(string email, string userName, EnrootRoles role)
    {
        Email = email;
        UserName = userName;
        RoleId = (int)role;
    }

    public int RoleId { get; private set; }
    public virtual Role Role { get; private set; } = null!;

    public static User Create(string email, string userName, EnrootRoles role)
    {
        return new User (email, userName, role);
    }
}