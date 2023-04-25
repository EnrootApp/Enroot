using Enroot.Domain.ReadEntities;
using Enroot.Domain.Tasq.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<StatusRead>
{
    public void Configure(EntityTypeBuilder<StatusRead> builder)
    {
        ConfigureAccountTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<StatusRead> builder)
    {
        builder.ToView("Statuses");

        builder.HasKey("Id", nameof(AssignmentId));

        builder.Ignore(a => a.DbId);
        builder.Ignore(a => a.IsDeleted);
    }
}