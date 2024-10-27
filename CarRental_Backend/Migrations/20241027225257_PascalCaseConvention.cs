using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental_Backend.Migrations
{
    /// <inheritdoc />
    public partial class PascalCaseConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Employee_Surname",
                table: "Employees",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Employee_Salary",
                table: "Employees",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "Employee_Position",
                table: "Employees",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "Employee_Phone",
                table: "Employees",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Employee_Name",
                table: "Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Employee_Email",
                table: "Employees",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Employee_Date_of_birth",
                table: "Employees",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Employee_Country",
                table: "Employees",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Employee_City",
                table: "Employees",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Employee_Address",
                table: "Employees",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "License_number",
                table: "Clients",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "License_issue_date",
                table: "Clients",
                newName: "LicenseIssueDate");

            migrationBuilder.RenameColumn(
                name: "Client_Surname",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Client_Phone",
                table: "Clients",
                newName: "LicenseNumber");

            migrationBuilder.RenameColumn(
                name: "Client_Name",
                table: "Clients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Client_Email",
                table: "Clients",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Client_Date_of_birth",
                table: "Clients",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Client_Country",
                table: "Clients",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Client_City",
                table: "Clients",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Client_Address",
                table: "Clients",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Car_class",
                table: "Cars",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Car_Year",
                table: "Cars",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Car_Type",
                table: "Cars",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Car_PricePerDay",
                table: "Cars",
                newName: "PricePerDay");

            migrationBuilder.RenameColumn(
                name: "Car_Model",
                table: "Cars",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "Car_Mileage",
                table: "Cars",
                newName: "Mileage");

            migrationBuilder.RenameColumn(
                name: "Car_Gear_is_automatic",
                table: "Cars",
                newName: "IsAutomatic");

            migrationBuilder.RenameColumn(
                name: "Car_Color",
                table: "Cars",
                newName: "Class");

            migrationBuilder.RenameColumn(
                name: "Car_Brand",
                table: "Cars",
                newName: "Brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Employees",
                newName: "Employee_Surname");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "Employee_Salary");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Employees",
                newName: "Employee_Position");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Employees",
                newName: "Employee_Phone");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employees",
                newName: "Employee_Name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employees",
                newName: "Employee_Email");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Employees",
                newName: "Employee_Date_of_birth");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Employees",
                newName: "Employee_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Employees",
                newName: "Employee_City");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Employees",
                newName: "Employee_Address");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Clients",
                newName: "License_number");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Clients",
                newName: "Client_Surname");

            migrationBuilder.RenameColumn(
                name: "LicenseNumber",
                table: "Clients",
                newName: "Client_Phone");

            migrationBuilder.RenameColumn(
                name: "LicenseIssueDate",
                table: "Clients",
                newName: "License_issue_date");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Clients",
                newName: "Client_Name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clients",
                newName: "Client_Email");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Clients",
                newName: "Client_Date_of_birth");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Clients",
                newName: "Client_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clients",
                newName: "Client_City");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "Client_Address");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Cars",
                newName: "Car_Year");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Cars",
                newName: "Car_class");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Cars",
                newName: "Car_PricePerDay");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "Car_Type");

            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "Cars",
                newName: "Car_Mileage");

            migrationBuilder.RenameColumn(
                name: "IsAutomatic",
                table: "Cars",
                newName: "Car_Gear_is_automatic");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Cars",
                newName: "Car_Model");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "Cars",
                newName: "Car_Color");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Cars",
                newName: "Car_Brand");
        }
    }
}
