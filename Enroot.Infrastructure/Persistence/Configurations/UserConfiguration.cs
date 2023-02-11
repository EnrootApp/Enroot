using Enroot.Domain.User;
using Enroot.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
        ConfigureUserAccountIdsTable(builder);
    }

    private void ConfigureUserAccountIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(user => user.AccountIds, accountIdBuilder =>
        {
            accountIdBuilder.ToTable("UserAccountIds");

            accountIdBuilder.WithOwner().HasForeignKey(nameof(UserId));

            accountIdBuilder.HasKey("Id");

            accountIdBuilder.Property(accountId => accountId.Value)
            .ValueGeneratedNever()
            .HasColumnName("AccountId");
        });

        builder.Metadata.FindNavigation(nameof(User.AccountIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.DbId);

        builder
           .Property(user => user.DbId)
           .ValueGeneratedOnAdd();

        builder
            .Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );

        builder
            .Property(user => user.Email)
            .HasConversion(
                email => email!.Value,
                value => Email.Create(value)
            );

        builder
            .Property(user => user.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber!.Value,
                value => PhoneNumber.Create(value)
            );
    }
}