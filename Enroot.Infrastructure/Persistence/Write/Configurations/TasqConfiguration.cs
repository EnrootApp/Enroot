using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.Tenant;
using Enroot.Domain.Account;
using Enroot.Domain.Tasq;
using Enroot.Domain.Tasq.ValueObjects.Statuses;

namespace Enroot.Infrastructure.Persistence.Write.Configurations;

public class TasqConfiguration : IEntityTypeConfiguration<Tasq>
{
    public void Configure(EntityTypeBuilder<Tasq> builder)
    {
        ConfigureAssignmentTable(builder);
        ConfigureTasqTable(builder);
    }

    private static void ConfigureTasqTable(EntityTypeBuilder<Tasq> builder)
    {
        builder.ToTable("Tasqs");
        builder.HasKey(t => t.DbId);

        builder.Property(t => t.Id)
               .ValueGeneratedNever()
               .HasColumnName("Id");

        builder.Ignore(t => t.IsCompleted);

        builder
            .Property(a => a.Id)
            .HasConversion(
                ai => ai!.Value,
                value => TasqId.Create(value)
            );

        builder
            .Property(a => a.CreatorId)
            .HasConversion(
                ai => ai!.Value,
                value => AccountId.Create(value)
            );

        builder
           .Property(a => a.TenantId)
           .HasConversion(
               ai => ai!.Value,
               value => TenantId.Create(value)
           );

        builder
           .Property(a => a.Description)
           .HasMaxLength(1000);

        builder
          .Property(a => a.Title)
          .HasMaxLength(100);

        builder.HasOne<Tenant>().WithMany().HasForeignKey(t => t.TenantId).HasPrincipalKey(t => t.Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<Account>().WithMany().HasForeignKey(t => t.CreatorId).HasPrincipalKey(a => a.Id).OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigureAssignmentTable(EntityTypeBuilder<Tasq> builder)
    {
        builder.OwnsMany(t => t.Assignments, assignmentsBuilder =>
        {
            assignmentsBuilder.ToTable("Assignments");

            assignmentsBuilder.WithOwner().HasPrincipalKey(t => t.Id);

            assignmentsBuilder.HasKey(a => a.DbId);

            assignmentsBuilder.Property(a => a.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            assignmentsBuilder
                .Property(a => a.Id)
                .HasConversion(
                    ai => ai!.Value,
                    value => AssignmentId.Create(value)
                );

            assignmentsBuilder
                .Property(a => a.AssigneeId)
                .HasConversion(
                    ai => ai!.Value,
                    value => AccountId.Create(value)
                );

            assignmentsBuilder
               .Property(a => a.AssignerId)
               .HasConversion(
                   ai => ai!.Value,
                   value => AccountId.Create(value)
               );

            assignmentsBuilder
                .Property(a => a.AssignerId)
                .HasConversion(
                    ai => ai!.Value,
                    value => AccountId.Create(value)
                );

            assignmentsBuilder.HasOne<Account>().WithMany().HasForeignKey(a => a.AssigneeId).HasPrincipalKey(a => a.Id).OnDelete(DeleteBehavior.NoAction);
            assignmentsBuilder.HasOne<Account>().WithMany().HasForeignKey(a => a.AssignerId).HasPrincipalKey(a => a.Id).OnDelete(DeleteBehavior.NoAction);

            assignmentsBuilder.OwnsMany(a => a.Attachments, attachmentBuilder =>
            {
                attachmentBuilder.ToTable("Attachments");

                attachmentBuilder.WithOwner().HasPrincipalKey(a => a.Id);

                attachmentBuilder.Property<int>("Id").UseIdentityColumn();
                attachmentBuilder.HasKey("Id", nameof(AssignmentId));

                attachmentBuilder.Property(at => at.Name).HasMaxLength(64).IsRequired();
                attachmentBuilder.Property(at => at.BlobUrl).IsRequired();
            });

            assignmentsBuilder.OwnsMany(a => a.Statuses, statusBuilder =>
            {
                statusBuilder.ToTable("Statuses");

                statusBuilder.WithOwner().HasPrincipalKey(a => a.Id);
                statusBuilder.HasKey("Id", nameof(AssignmentId));

                statusBuilder
                    .Property(s => s.Id)
                    .HasConversion(s => s!.Value, value => StatusBase.Create(value));

                statusBuilder
                    .Property(s => s.Feedback)
                    .HasConversion(s => s!.Value, value => FeedbackMessage.Create(value).Value)
                    .HasMaxLength(255);

                statusBuilder
                    .Property(s => s.CreatorId)
                    .HasConversion(s => s!.Value, value => AccountId.Create(value));
            });

            assignmentsBuilder.Navigation(nameof(Assignment.Statuses)).UsePropertyAccessMode(PropertyAccessMode.Field);
            assignmentsBuilder.Navigation(nameof(Assignment.Attachments)).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Tasq.Assignments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
