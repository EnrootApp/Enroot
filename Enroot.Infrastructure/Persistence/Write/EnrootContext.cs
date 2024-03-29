﻿using Microsoft.EntityFrameworkCore;
using Enroot.Domain.Account;
using Enroot.Domain.Permission;
using Enroot.Domain.Role;
using Enroot.Domain.Tasq;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.Tenant;
using Enroot.Domain.User;

namespace Enroot.Infrastructure.Persistence.Write;

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
    public DbSet<Tasq> Tasqs { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnrootContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}