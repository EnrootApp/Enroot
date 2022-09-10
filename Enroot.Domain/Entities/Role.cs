using Enroot.Domain.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        private readonly List<User>? _users;
        private readonly List<RoleClaim>? _claims;

        protected internal Role () { }

        public Role(EnrootRoles role)
        {
            Id = (int)role;
        }

        public virtual IReadOnlyCollection<User>? Users => _users;
        public virtual IReadOnlyCollection<RoleClaim>? Claims => _claims;
    }
}
