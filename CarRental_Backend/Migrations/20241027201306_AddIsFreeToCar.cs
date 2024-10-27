using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFreeToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car_Price",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Car_Free",
                table: "Cars",
                newName: "IsFree");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rental_price",
                table: "Rentals",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Rentals",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Employee_id",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Rentals",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Return_date_actual",
                table: "Rentals",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Emplotyee_Salary",
                table: "Employees",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Employees",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Car_PricePerDay",
                table: "Cars",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals",
                column: "Employee_id",
                principalTable: "Employees",
                principalColumn: "Employee_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_Employee_id",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_Employee_id",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Employee_id",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Return_date_actual",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Car_PricePerDay",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "IsFree",
                table: "Cars",
                newName: "Car_Free");

            migrationBuilder.AlterColumn<int>(
                name: "Rental_price",
                table: "Rentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<int>(
                name: "Emplotyee_Salary",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<int>(
                name: "Car_Price",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
