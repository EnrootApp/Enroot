﻿// <auto-generated />
using System;
using Enroot.Domain.Role.Enums;
using Enroot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enroot.Infrastructure.Migrations
{
    [DbContext(typeof(EnrootContext))]
    [Migration("20230210093954_Add_UserRole")]
    partial class AddUserRole
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
                        });
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

                    b.Property<string>("Name")
                        .IsRequired()
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
                    b.OwnsMany("Enroot.Domain.Role.ValueObjects.RolePermissionId", "Permissions", b1 =>
                        {
                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.Property<int>("RoleId")
                                .HasColumnType("int");

                            b1.HasKey("Value")
                                .HasName("Id");

                            b1.HasIndex("RoleId");

                            b1.ToTable("RolePermissions", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Enroot.Domain.Tenant.Tenant", b =>
                {
                    b.OwnsMany("Enroot.Domain.Account.ValueObjects.AccountId", "AccountIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("TenantId")
                                .HasColumnType("int");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AccountId");

                            b1.HasKey("Id");

                            b1.HasIndex("TenantId");

                            b1.ToTable("TenantAccountIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TenantId");
                        });

                    b.Navigation("AccountIds");
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
#pragma warning restore 612, 618
        }
    }
}
