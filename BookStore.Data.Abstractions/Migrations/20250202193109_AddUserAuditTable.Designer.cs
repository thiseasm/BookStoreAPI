﻿// <auto-generated />
using System;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Data.Abstractions.Migrations
{
    [DbContext(typeof(BookStoreContext))]
    [Migration("20250202193109_AddUserAuditTable")]
    partial class AddUserAuditTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.BookDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.CategoryDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Science"
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 6,
                            IsActive = true,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 7,
                            IsActive = true,
                            Name = "Poetry"
                        },
                        new
                        {
                            Id = 8,
                            IsActive = true,
                            Name = "Fantasy"
                        });
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.RoleDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "IT"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Moderator"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Test"
                        },
                        new
                        {
                            Id = 5,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.UserLogDto", b =>
                {
                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Timestamp", "EntityId");

                    b.ToTable("UserLogs", (string)null);
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.UserRoleDto", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.BookDto", b =>
                {
                    b.HasOne("BookStore.Infrastructure.DTOs.CategoryDto", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookStore.Infrastructure.DTOs.UserRoleDto", b =>
                {
                    b.HasOne("BookStore.Infrastructure.DTOs.RoleDto", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Infrastructure.DTOs.UserDto", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
