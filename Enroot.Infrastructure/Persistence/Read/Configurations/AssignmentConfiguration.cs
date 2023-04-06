using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<AssignmentRead>
{
    public void Configure(EntityTypeBuilder<AssignmentRead> builder)
    {
        ConfigureAssignmentTable(builder);
    }

    private static void ConfigureAssignmentTable(EntityTypeBuilder<AssignmentRead> builder)
    {
        builder.ToView("Assignments");
        builder.HasMany(t => t.Attachments).WithOne(a => a.Assignment).HasPrincipalKey(t => t.Id).HasForeignKey(t => t.AssignmentId);
    }
}
