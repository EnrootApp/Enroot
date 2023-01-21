using Enroot.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence;

public class EnrootContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public EnrootContext(DbContextOptions<EnrootContext> options) : base(options)
    {
    }
}