using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Enroot.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Enroot.Infrastructure.Persistence;

public class EnrootContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public EnrootContext(DbContextOptions<EnrootContext> options) : base(options)
    {
    }
}