using Enroot.Domain.Account;
using Enroot.Domain.Permission;
using Enroot.Domain.Role;
using Enroot.Domain.Tenant;
using Enroot.Domain.User;
using Enroot.Infrastructure.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence;

public class EnrootContext : DbContext
{
    public EnrootContext(DbContextOptions<EnrootContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnrootContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}