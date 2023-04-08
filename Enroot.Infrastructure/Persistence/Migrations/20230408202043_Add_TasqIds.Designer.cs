﻿// <auto-generated />
using System;
using Enroot.Infrastructure.Persistence.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Tasq.Enums;
#nullable disable

namespace Enroot.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(EnrootContext))]
    [Migration("20230408202043_Add_TasqIds")]
    partial class AddTasqIds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Enroot.Domain.Account.Account", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<RoleEnum>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DbId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("TenantId", "UserId")
                        .IsUnique();

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.Permission.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3380)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3420)
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(3450)
                        });
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AccountRead", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DbId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable((string)null);

                    b.ToView("Accounts", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AssignmentRead", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApproverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssigneeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DbId")
                        .HasColumnType("int");

                    b.Property<string>("FeedbackMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TasqId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("AssignerId");

                    b.HasIndex("TasqId");

                    b.ToTable((string)null);

                    b.ToView("Assignments", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AttachmentRead", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlobUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DbId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable((string)null);

                    b.ToView("Attachments", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.PermissionRead", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("Permissions", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.RolePermissionRead", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable((string)null);

                    b.ToView("RolePermissions", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.RoleRead", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("Roles", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.TasqRead", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DbId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable((string)null);

                    b.ToView("Tasqs", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.UserRead", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DbId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("Users", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.Role.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5160)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5200)
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5230)
                        },
                        new
                        {
                            Id = 4,
                            CreatedOn = new DateTime(2023, 4, 8, 20, 20, 43, 21, DateTimeKind.Utc).AddTicks(5250)
                        });
                });

            modelBuilder.Entity("Enroot.Domain.Tasq.Tasq", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DbId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TenantId");

                    b.ToTable("Tasqs", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.Tenant.Tenant", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DbId");

                    b.ToTable("Tenants", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.User.User", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DbId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.Account.Account", b =>
                {
                    b.HasOne("Enroot.Domain.Role.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.Tenant.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AccountRead", b =>
                {
                    b.HasOne("Enroot.Domain.ReadEntities.RoleRead", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.ReadEntities.UserRead", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AssignmentRead", b =>
                {
                    b.HasOne("Enroot.Domain.ReadEntities.AccountRead", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.ReadEntities.AccountRead", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.ReadEntities.AccountRead", "Assigner")
                        .WithMany()
                        .HasForeignKey("AssignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.ReadEntities.TasqRead", "Tasq")
                        .WithMany("Assignments")
                        .HasForeignKey("TasqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Assignee");

                    b.Navigation("Assigner");

                    b.Navigation("Tasq");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AttachmentRead", b =>
                {
                    b.HasOne("Enroot.Domain.ReadEntities.AssignmentRead", "Assignment")
                        .WithMany("Attachments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.RolePermissionRead", b =>
                {
                    b.HasOne("Enroot.Domain.ReadEntities.PermissionRead", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.ReadEntities.RoleRead", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.TasqRead", b =>
                {
                    b.HasOne("Enroot.Domain.ReadEntities.AccountRead", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Enroot.Domain.Role.Role", b =>
                {
                    b.OwnsMany("Enroot.Domain.Permission.ValueObjects.PermissionId", "Permissions", b1 =>
                        {
                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("PermissionId");

                            b1.Property<int>("RoleId")
                                .HasColumnType("int");

                            b1.HasKey("Value", "RoleId");

                            b1.HasIndex("RoleId");

                            b1.ToTable("RolePermissions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoleId");

                            b1.HasData(
                                new
                                {
                                    Value = 1,
                                    RoleId = 1
                                },
                                new
                                {
                                    Value = 2,
                                    RoleId = 1
                                },
                                new
                                {
                                    Value = 3,
                                    RoleId = 1
                                },
                                new
                                {
                                    Value = 2,
                                    RoleId = 3
                                });
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Enroot.Domain.Tasq.Tasq", b =>
                {
                    b.HasOne("Enroot.Domain.Account.Account", null)
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.Tenant.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsMany("Enroot.Domain.Tasq.Entities.Assignment", "Assignments", b1 =>
                        {
                            b1.Property<int>("DbId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("DbId"));

                            b1.Property<Guid?>("ApproverId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("AssigneeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("AssignerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedOn")
                                .HasColumnType("datetime2");

                            b1.Property<string>("FeedbackMessage")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Status>("Status")
                                .HasColumnType("int");

                            b1.Property<Guid>("TasqId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("DbId");

                            b1.HasIndex("ApproverId");

                            b1.HasIndex("AssigneeId");

                            b1.HasIndex("AssignerId");

                            b1.HasIndex("TasqId");

                            b1.ToTable("Assignments", (string)null);

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithMany()
                                .HasForeignKey("ApproverId")
                                .HasPrincipalKey("Id")
                                .OnDelete(DeleteBehavior.NoAction);

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithMany()
                                .HasForeignKey("AssigneeId")
                                .HasPrincipalKey("Id")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithMany()
                                .HasForeignKey("AssignerId")
                                .HasPrincipalKey("Id")
                                .OnDelete(DeleteBehavior.NoAction)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TasqId")
                                .HasPrincipalKey("Id");

                            b1.OwnsMany("Enroot.Domain.Tasq.ValueObjects.Attachment", "Attachments", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<Guid>("AssignmentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("BlobUrl")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(64)
                                        .HasColumnType("nvarchar(64)");

                                    b2.HasKey("Id", "AssignmentId");

                                    b2.HasIndex("AssignmentId");

                                    b2.ToTable("Attachments", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("AssignmentId")
                                        .HasPrincipalKey("Id");
                                });

                            b1.Navigation("Attachments");
                        });

                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("Enroot.Domain.Tenant.Tenant", b =>
                {
                    b.OwnsOne("Enroot.Domain.Tenant.ValueObjects.TenantName", "Name", b1 =>
                        {
                            b1.Property<int>("TenantDbId")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(62)
                                .HasColumnType("nvarchar(62)")
                                .HasColumnName("Name");

                            b1.HasKey("TenantDbId");

                            b1.ToTable("Tenants");

                            b1.WithOwner()
                                .HasForeignKey("TenantDbId");
                        });

                    b.OwnsMany("Enroot.Domain.Tasq.ValueObjects.TasqId", "TasqIds", b1 =>
                        {
                            b1.Property<int>("DbId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("DbId"));

                            b1.Property<Guid>("TenantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("TasqId");

                            b1.HasKey("DbId");

                            b1.HasIndex("TenantId");

                            b1.ToTable("TenantTasqIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TenantId")
                                .HasPrincipalKey("Id");
                        });

                    b.OwnsMany("Enroot.Domain.Account.ValueObjects.AccountId", "AccountIds", b1 =>
                        {
                            b1.Property<int>("DbId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("DbId"));

                            b1.Property<Guid?>("TenantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AccountId");

                            b1.HasKey("DbId");

                            b1.HasIndex("TenantId");

                            b1.ToTable("TenantAccountIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TenantId")
                                .HasPrincipalKey("Id");
                        });

                    b.Navigation("AccountIds");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("TasqIds");
                });

            modelBuilder.Entity("Enroot.Domain.User.User", b =>
                {
                    b.OwnsMany("Enroot.Domain.Account.ValueObjects.AccountId", "AccountIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AccountId");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserAccountIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("AccountIds");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.AssignmentRead", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.RoleRead", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.TasqRead", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("Enroot.Domain.ReadEntities.UserRead", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
