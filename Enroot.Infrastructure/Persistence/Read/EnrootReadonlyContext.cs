﻿using Microsoft.EntityFrameworkCore;
using Enroot.Domain.ReadEntities;

namespace Enroot.Infrastructure.Persistence.Read;

public class EnrootReadonlyContext : DbContext
{
    public EnrootReadonlyContext(DbContextOptions<EnrootReadonlyContext> options) : base(options)
    {
    }

    public DbSet<UserRead> Users { get; set; } = null!;
    public DbSet<AccountRead> Accounts { get; set; } = null!;
    public DbSet<TasqRead> Tasqs { get; set; } = null!;
    public DbSet<AssignmentRead> Assignments { get; set; } = null!;
    public DbSet<AttachmentRead> Attachments { get; set; } = null!;
    public DbSet<RoleRead> Roles { get; set; }
    public DbSet<PermissionRead> Permissions { get; set; }
    public DbSet<StatusRead> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnrootReadonlyContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}