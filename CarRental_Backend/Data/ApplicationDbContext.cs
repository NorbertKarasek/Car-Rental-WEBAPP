using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CarRental_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee_id in Employees
            modelBuilder.Entity<Employees>()
                .Property(e => e.Employee_id)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            // Configure Employee_id in Rentals
            modelBuilder.Entity<Rentals>()
                .Property(r => r.Employee_id)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            // Configure Client_id in Clients
            modelBuilder.Entity<Clients>()
                .Property(c => c.Client_id)
                .HasColumnType("varchar(255)");

            // Configure Client_id in Rentals
            modelBuilder.Entity<Rentals>()
                .Property(r => r.Client_id)
                .HasColumnType("varchar(255)");

            // Relation 1:N between Rentals and Employees
            modelBuilder.Entity<Rentals>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.Employee_id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation 1:N between Rentals and Clients
            modelBuilder.Entity<Rentals>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.Client_id)
                .IsRequired();

            // Relation 1:N between Rentals and Cars
            modelBuilder.Entity<Rentals>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.Car_id);

            // Relation 1:1 between ApplicationUser and Clients
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Client)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Clients>(c => c.ApplicationUserId);

            // Relation 1:1 between ApplicationUser and Employees
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Employee)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<Employees>(e => e.ApplicationUserId);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cars> Cars { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Rentals> Rentals { get; set; }

        // more dbsets
    }

}

