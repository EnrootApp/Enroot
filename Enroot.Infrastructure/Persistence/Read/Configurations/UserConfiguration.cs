using Enroot.Domain.ReadEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Read.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserRead>
{
    public void Configure(EntityTypeBuilder<UserRead> builder)
    {
        ConfigureUserTable(builder);
    }

    private static void ConfigureUserTable(EntityTypeBuilder<UserRead> builder)
    {
        builder.ToView("Users");
    }
}