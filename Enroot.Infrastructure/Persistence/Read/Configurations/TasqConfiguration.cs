using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class TasqConfiguration : IEntityTypeConfiguration<TasqRead>
{
    public void Configure(EntityTypeBuilder<TasqRead> builder)
    {
        ConfigureTasqTable(builder);
    }

    private static void ConfigureTasqTable(EntityTypeBuilder<TasqRead> builder)
    {
        builder.ToView("Tasqs");

        builder.HasMany(t => t.Assignments).WithOne(a => a.Tasq).HasPrincipalKey(t => t.Id).HasForeignKey(t => t.TasqId);
    }
}
