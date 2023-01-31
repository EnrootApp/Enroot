using Enroot.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence;

public class EnrootContext : DbContext
{
    public EnrootContext(DbContextOptions<EnrootContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnrootContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}