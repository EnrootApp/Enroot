using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; private set; } = null!;
    }
}
