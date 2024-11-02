﻿// <auto-generated />
using System;
using CarRental_Backend.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241102184022_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CarId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAutomatic")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFree")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("CarRental_Backend.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CarRental_Backend.Models.Client", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LicenseIssueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ClientId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Client");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Position")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Rental", b =>
                {
                    b.Property<int>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RentalId"));

                    b.Property<decimal>("AdditionalFees")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("RentalDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("RentalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ReturnDateActual")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RentalId");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Client", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Client")
                        .HasForeignKey("CarRental_Backend.Models.Client", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Employee", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Employee")
                        .HasForeignKey("CarRental_Backend.Models.Employee", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental_Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rental", b =>
                {
                    b.HasOne("Car", "Car")
                        .WithMany("Rental")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental_Backend.Models.Client", "Client")
                        .WithMany("Rental")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental_Backend.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Car", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRental_Backend.Models.ApplicationUser", b =>
                {
                    b.Navigation("Client")
                        .IsRequired();

                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental_Backend.Models.Client", b =>
                {
                    b.Navigation("Rental");
                });
#pragma warning restore 612, 618
        }
    }
}