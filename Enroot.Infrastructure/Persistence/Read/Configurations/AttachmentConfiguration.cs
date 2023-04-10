using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentRead>
{
    public void Configure(EntityTypeBuilder<AttachmentRead> builder)
    {
        ConfigureAccountTable(builder);
    }

    private static void ConfigureAccountTable(EntityTypeBuilder<AttachmentRead> builder)
    {
        builder.ToView("Attachments");

        builder.Property(a => a.Id);

        builder.Ignore(a => a.DbId);
        builder.Ignore(a => a.CreatedOn);
    }
}