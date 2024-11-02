using CarRental_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental_Backend.Data.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(r => r.EmployeeId)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(r => r.ClientId)
                .HasColumnType("varchar(255)");

            // Relation 1:N between Rental and Employee
            builder.HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation 1:N between Rental and Client
            builder.HasOne(r => r.Client)
                .WithMany(c => c.Rental)
                .HasForeignKey(r => r.ClientId)
                .IsRequired();

            // Relation 1:N between Rental and Car
            builder.HasOne(r => r.Car)
                .WithMany(c => c.Rental)
                .HasForeignKey(r => r.CarId);
        }
    }
}
