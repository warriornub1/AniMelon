﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneLearn.Infrastructure.Common.DBContexts;

#nullable disable

namespace OneLearn.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OneLearn.Domain.Test.Test", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("a")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("b")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("c")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("d")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("e")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("f")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("g")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("h")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("i")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("j")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("k")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("l")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("m")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("n")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Tests");

                    b.HasData(
                        new
                        {
                            id = 1,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 2,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 3,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 4,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 5,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 6,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 7,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 8,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 9,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        },
                        new
                        {
                            id = 10,
                            a = "a",
                            b = "b",
                            c = "c",
                            created_by = "asd",
                            created_on = new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            d = "d",
                            e = "e",
                            f = "f",
                            g = "g",
                            h = "h",
                            i = "i",
                            j = "j",
                            k = "k",
                            l = "l",
                            m = "m",
                            n = "n"
                        });
                });

            modelBuilder.Entity("OneLearn.Domain.User.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OneLearn.Domain.User.RolePrivillege", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("RolePrivilegeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePrivileges");
                });

            modelBuilder.Entity("OneLearn.Domain.User.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OneLearn.Domain.User.UserRoleMapping", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("OneLearn.Domain.User.UserType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("OneLearn.Domain.VoiceTranslation.Language", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("language")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("language_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("language", "language_Code")
                        .IsUnique();

                    b.ToTable("Language");
                });

            modelBuilder.Entity("OneLearn.Domain.VoiceTranslation.Passage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("created_by")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<int>("langauge_id")
                        .HasColumnType("int");

                    b.Property<string>("last_modified_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_modified_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("passage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("langauge_id");

                    b.ToTable("Passages");
                });

            modelBuilder.Entity("OneLearn.Domain.User.RolePrivillege", b =>
                {
                    b.HasOne("OneLearn.Domain.User.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OneLearn.Domain.User.User", b =>
                {
                    b.HasOne("OneLearn.Domain.User.UserType", null)
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OneLearn.Domain.User.UserRoleMapping", b =>
                {
                    b.HasOne("OneLearn.Domain.User.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneLearn.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OneLearn.Domain.VoiceTranslation.Passage", b =>
                {
                    b.HasOne("OneLearn.Domain.VoiceTranslation.Language", null)
                        .WithMany()
                        .HasForeignKey("langauge_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
