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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>()
            .HasMany(e => e.Users)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Role>()
            .HasMany(e => e.Claims)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId);

        modelBuilder.Entity<RoleClaim>()
            .HasOne(e => e.Role)
            .WithMany(e => e.Claims);

        modelBuilder.Entity<Role>().HasData(
            new Role(EnrootRoles.Admin),
            new Role(EnrootRoles.User));

        modelBuilder.Entity<RoleClaim>().HasData(
           new RoleClaim
           {
               Id = (int)RolePermissions.AssignRoles,
               RoleId = 1,
           }
       );
    }
}