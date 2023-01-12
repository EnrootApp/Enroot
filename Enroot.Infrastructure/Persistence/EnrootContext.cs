using Enroot.Domain.Common.Enums;
using Enroot.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence;

public class EnrootContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, RoleClaim, IdentityUserToken<int>>
{
    public EnrootContext(DbContextOptions<EnrootContext> options) : base(options)
    {
    }
}