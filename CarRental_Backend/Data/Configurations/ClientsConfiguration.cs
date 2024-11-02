using CarRental_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental_Backend.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.ClientId)
                .HasColumnType("varchar(255)");

            // Relation 1:1 between ApplicationUser and Client
            builder.HasOne(c => c.ApplicationUser)
                .WithOne(a => a.Client)
                .HasForeignKey<Client>(c => c.ApplicationUserId);
        }
    }
}
