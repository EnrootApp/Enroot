using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual IEnumerable<User> Users { get; set; } = null!;
        public virtual IEnumerable<RoleClaim> Claims { get; set; } = null!;
    }
}
