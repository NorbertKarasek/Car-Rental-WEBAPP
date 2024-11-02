using CarRental_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental_Backend.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.EmployeeId)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            // Relation 1:1 between ApplicationUser and Employee
            builder.HasOne(e => e.ApplicationUser)
                .WithOne(a => a.Employee)
                .HasForeignKey<Employee>(e => e.ApplicationUserId);
        }
    }
}
