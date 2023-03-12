﻿// <auto-generated />
using System;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Tasq.Enums;
using Enroot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enroot.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(EnrootContext))]
    partial class EnrootContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        });
                });

            modelBuilder.Entity("Enroot.Domain.Role.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        },
                        new
                        {
                            Id = 4
                        });
                });

            modelBuilder.Entity("Enroot.Domain.Tasq.Tasq", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

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

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.HasKey("DbId");

                    b.ToTable("Tenants", (string)null);
                });

            modelBuilder.Entity("Enroot.Domain.User.User", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DbId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
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
                                },
                                new
                                {
                                    Value = 4,
                                    RoleId = 2
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enroot.Domain.Tenant.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Enroot.Domain.Tasq.Entities.Assignment", "Assignments", b1 =>
                        {
                            b1.Property<int>("DbId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("DbId"));

                            b1.Property<Guid>("AssigneeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("AssignerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FeedbackMessage")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Status>("Status")
                                .HasColumnType("int");

                            b1.Property<int>("TasqId")
                                .HasColumnType("int");

                            b1.HasKey("DbId");

                            b1.HasIndex("AssigneeId");

                            b1.HasIndex("AssignerId");

                            b1.HasIndex("TasqId");

                            b1.ToTable("Assignments", (string)null);

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithMany()
                                .HasForeignKey("AssigneeId")
                                .HasPrincipalKey("Id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithMany()
                                .HasForeignKey("AssignerId")
                                .HasPrincipalKey("Id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TasqId");

                            b1.OwnsMany("Enroot.Domain.Tasq.ValueObjects.Attachment", "Attachments", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("AssignmentId")
                                        .HasColumnType("int");

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
                                        .HasForeignKey("AssignmentId");
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
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("TenantDbId");

                            b1.ToTable("Tenants");

                            b1.WithOwner()
                                .HasForeignKey("TenantDbId");
                        });

                    b.OwnsMany("Enroot.Domain.Account.ValueObjects.AccountId", "AccountIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<int>("TenantId")
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AccountId");

                            b1.HasKey("Id");

                            b1.HasIndex("TenantId");

                            b1.ToTable("TenantAccountIds", (string)null);

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithOne()
                                .HasForeignKey("Enroot.Domain.Tenant.Tenant.AccountIds#Enroot.Domain.Account.ValueObjects.AccountId", "Id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TenantId");
                        });

                    b.Navigation("AccountIds");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Enroot.Domain.User.User", b =>
                {
                    b.OwnsMany("Enroot.Domain.Account.ValueObjects.AccountId", "AccountIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AccountId");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserAccountIds", (string)null);

                            b1.HasOne("Enroot.Domain.Account.Account", null)
                                .WithOne()
                                .HasForeignKey("Enroot.Domain.User.User.AccountIds#Enroot.Domain.Account.ValueObjects.AccountId", "Id")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("AccountIds");
                });
#pragma warning restore 612, 618
        }
    }
}
