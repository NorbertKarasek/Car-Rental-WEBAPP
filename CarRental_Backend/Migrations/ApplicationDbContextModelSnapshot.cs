﻿// <auto-generated />
using System;
using CarRental_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRental_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

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
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("CarRental_Backend.Models.Clients", b =>
                {
                    b.Property<int>("Client_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Client_id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Client_Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Client_City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Client_Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Client_Date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Client_Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Client_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Client_Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Client_Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("License_issue_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("License_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Client_id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Employees", b =>
                {
                    b.Property<int>("Employee_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Employee_id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Emplotyee_Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Emplotyee_City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Emplotyee_Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Emplotyee_Date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Emplotyee_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Emplotyee_Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Emplotyee_Position")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Emplotyee_Salary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Emplotyee_Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Employee_Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Employee_id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Cars", b =>
                {
                    b.Property<int>("Car_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Car_id"));

                    b.Property<string>("Car_Brand")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Car_Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Car_Gear_is_automatic")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Car_Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Car_Model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Car_PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Car_Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Car_Year")
                        .HasColumnType("int");

                    b.Property<string>("Car_class")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsFree")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Car_id");

                    b.ToTable("Cars");
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

            modelBuilder.Entity("Rentals", b =>
                {
                    b.Property<int>("Rental_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Rental_id"));

                    b.Property<int>("Car_id")
                        .HasColumnType("int");

                    b.Property<int>("Client_id")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("Employee_id")
                        .HasColumnType("int");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Rental_date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Rental_price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("Return_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Return_date_actual")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Rental_id");

                    b.HasIndex("Car_id");

                    b.HasIndex("Client_id");

                    b.HasIndex("Employee_id");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Clients", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Client")
                        .HasForeignKey("CarRental_Backend.Models.Clients", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("CarRental_Backend.Models.Employees", b =>
                {
                    b.HasOne("CarRental_Backend.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
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

            modelBuilder.Entity("Rentals", b =>
                {
                    b.HasOne("Cars", "Car")
                        .WithMany("Rentals")
                        .HasForeignKey("Car_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental_Backend.Models.Clients", "Client")
                        .WithMany("Rentals")
                        .HasForeignKey("Client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRental_Backend.Models.Employees", "Employee")
                        .WithMany()
                        .HasForeignKey("Employee_id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Car");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CarRental_Backend.Models.ApplicationUser", b =>
                {
                    b.Navigation("Client")
                        .IsRequired();
                });

            modelBuilder.Entity("CarRental_Backend.Models.Clients", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("Cars", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
