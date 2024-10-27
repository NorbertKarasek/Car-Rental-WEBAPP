using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalFeesToRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalFees",
                table: "Rentals",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalFees",
                table: "Rentals");
        }
    }
}
