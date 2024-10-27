using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CarRental_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
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
