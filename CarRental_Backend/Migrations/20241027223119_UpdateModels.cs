using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental_Backend.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Clients_Client_id",
                table: "Rentals");

            // Drop indexes
            migrationBuilder.DropIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_Client_id",
                table: "Rentals");

            // Update data to remove nulls
            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Employee_id",
                keyValue: null,
                column: "Employee_id",
                value: "");

            // Alter columns in Rentals
            migrationBuilder.AlterColumn<string>(
                name: "Employee_id",
                table: "Rentals",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Client_id",
                table: "Rentals",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            // Alter columns in Employees
            migrationBuilder.AlterColumn<string>(
                name: "Employee_id",
                table: "Employees",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            // Alter columns in Clients
            migrationBuilder.AlterColumn<string>(
                name: "Client_id",
                table: "Clients",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            // Re-create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Client_id",
                table: "Rentals",
                column: "Client_id");

            // Re-add foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals",
                column: "Employee_id",
                principalTable: "Employees",
                principalColumn: "Employee_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Clients_Client_id",
                table: "Rentals",
                column: "Client_id",
                principalTable: "Clients",
                principalColumn: "Client_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Clients_Client_id",
                table: "Rentals");

            // Drop indexes
            migrationBuilder.DropIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_Client_id",
                table: "Rentals");

            // Alter columns back to original types
            migrationBuilder.AlterColumn<int>(
                name: "Employee_id",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "Rentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Employee_id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            // Re-create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Client_id",
                table: "Rentals",
                column: "Client_id");

            // Re-add foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals",
                column: "Employee_id",
                principalTable: "Employees",
                principalColumn: "Employee_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Clients_Client_id",
                table: "Rentals",
                column: "Client_id",
                principalTable: "Clients",
                principalColumn: "Client_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
