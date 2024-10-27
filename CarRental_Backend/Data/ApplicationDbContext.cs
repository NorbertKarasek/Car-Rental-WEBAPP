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


            modelBuilder.Entity<Rentals>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.Car_id);

            modelBuilder.Entity<Rentals>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.Client_id);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Client)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Clients>(c => c.ApplicationUserId);
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

