# CarRental_Backend

## Table of Contents
- [Project Description](#project-description)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
  - [appsettings.json](#appsettingsjson)
  - [Environment Variables](#environment-variables)
- [Data Models](#data-models)
- [Data Transfer Objects (DTO)](#data-transfer-objects-dto)
- [Services](#services)
- [Controllers](#controllers)
- [Authentication and Authorization](#authentication-and-authorization)
- [Database Initialization](#database-initialization)
- [Entity Configurations](#entity-configurations)
- [Extensions](#extensions)
- [Runtime Environment](#runtime-environment)
- [Additional Information](#additional-information)
- [How to Run the Project](#how-to-run-the-project)

---

## Project Description

**CarRental_Backend** is a web application designed to manage a car rental service. The project offers a wide range of functionalities, including:

- User registration and login (both clients and employees)
- Managing user profiles
- Browsing available cars
- Reserving and renting cars
- Managing rentals
- Granting discounts and calculating additional fees
- Authentication and authorization using JWT

## Technologies

The project is built using the following technologies:

- **.NET 6 (ASP.NET Core 6)**
- **Entity Framework Core 6**
- **MySQL Server**
- **Identity Framework**
- **JWT Authentication**
- **Swagger (Swashbuckle)**
- **C# 10**

## Project Structure

```
CarRental_Backend/
├── Controllers/
│   └── (API Controllers)
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── DbInitializer.cs
│   └── Configurations/
│       └── (Entity Configurations)
├── DTO/
│   └── (Data Transfer Objects)
├── Extensions/
│   ├── ApplicationBuilderExtensions.cs
│   └── ServiceExtensions.cs
├── Models/
│   └── (Data Models)
├── Services/
│   └── TokenService.cs
├── appsettings.json
└── Program.cs
```

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Car_rental;"
  },
  "JwtSettings": {
    "Issuer": "CarRentalAPI",
    "Audience": "CarRentalAPIUser",
    "DurationInMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Note**: Sensitive information such as `User Id`, `Password`, and `Key` are stored in environment variables.

### Environment Variables

- `ConnectionStrings__DefaultConnection`: Full connection string to the database, including `User Id` and `Password`.
- `JwtSettings__Key`: Secret key used for generating JWT tokens.

## Data Models

### `ApplicationUser`

Represents a system user.

```csharp
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public Client Client { get; set; }
    public Employee Employee { get; set; }
}
```

### `Client`

Represents a client.

```csharp
public class Client
{
    [Key]
    public string ClientId { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    // ... other properties
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
```

### `Employee`

Represents an employee.

```csharp
public class Employee
{
    [Key]
    public string EmployeeId { get; set; }
    
    [Required]
    public string ApplicationUserId { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; }
    
    // ... other properties
}
```

### `Car`

Represents a car.

```csharp
public class Car
{
    [Key]
    public int CarId { get; set; }
    
    [Required]
    public string Brand { get; set; }
    
    // ... other properties
    
    public ICollection<Rental> Rentals { get; set; }
}
```

### `Rental`

Represents a rental.

```csharp
public class Rental
{
    [Key]
    public int RentalId { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public string ClientId { get; set; }
    public Client Client { get; set; }
    
    // ... other properties
}
```

## Data Transfer Objects (DTO)

### `CarDTO`

```csharp
public class CarDTO
{
    public int CarId { get; set; }
    public string Brand { get; set; }
    
    // ... other properties
}
```

### `ClientDTO`

```csharp
public class ClientDTO
{
    public string ClientId { get; set; }
    public string FirstName { get; set; }
    
    // ... other properties
}
```

### `RentalDTO`

```csharp
public class RentalDTO
{
    public int RentalId { get; set; }
    public int CarId { get; set; }
    public CarDTO Car { get; set; }
    public string ClientId { get; set; }
    public ClientDTO Client { get; set; }
    
    // ... other properties
}
```

## Services

### `TokenService.cs`

Service responsible for generating JWT tokens.

```csharp
public class TokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        // Implementation for generating JWT token
    }
}
```

## Controllers

API controllers handle HTTP requests and manage business logic:

- **AuthController**: User registration and login.
- **CarController**: Managing cars.
- **RentalController**: Managing rentals.
- **ClientController**: Managing client profiles.
- **EmployeeController**: Managing employee profiles.

## Authentication and Authorization

- **Authentication**: JWT; the JWT token must be included in subsequent requests in the `Authorization: Bearer {token}` header.
- **Authorization**: Roles (Administrator, Employee, Client) control access to resources.

## Database Initialization

The `DbInitializer.cs` file is responsible for:

- Creating roles in the system (Administrator, Employee, Client).
- Creating an administrator account with default data.
- Adding initial data to the database (if required).

```csharp
public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // Implementation for initializing the database
    }
}
```

## Entity Configurations

Entity configurations are located in the `Data/Configurations/` directory and contain configurations for Entity Framework Core.

### Example: `RentalsConfiguration.cs`

```csharp
public class RentalsConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        // Configuration for Rental entity properties and relationships
    }
}
```

## Extensions

### `ServiceExtensions.cs`

Contains extension methods for configuring services:

- **ConfigureDatabase**: Database configuration.
- **ConfigureIdentity**: Identity Framework configuration.
- **ConfigureJWT**: JWT authentication configuration.
- **ConfigureServices**: Registration of custom services.
- **ConfigureCors**: CORS configuration.
- **ConfigureSwagger**: Swagger configuration.

### `ApplicationBuilderExtensions.cs`

Contains extension methods for configuring the application.

- **SeedDatabaseAsync**: Initializes the database during application startup.

## Runtime Environment

The `Program.cs` file is the main entry point of the application.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add environment variables
builder.Configuration
    .AddEnvironmentVariables();

// Configure services
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();

// Build the application
var app = builder.Build();

// Initialize the database and roles
await app.SeedDatabaseAsync();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

## Additional Information

- **Database Migrations**: Run migrations to update the database structure according to the models.

    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```

- **Security**: Sensitive data is stored in environment variables or tools like User Secrets during development.
- **Swagger**: Available at `/swagger` in the development environment, allowing API testing.
- **Password Configuration**: Password requirements can be adjusted in the `ConfigureIdentity` method in `ServiceExtensions.cs`.
- **Error Handling**: It's recommended to implement global error handling and logging.

---

## How to Run the Project

1. **Clone the Repository**
    ```bash
    git clone https://github.com/your-repository/CarRental_Backend.git
    cd CarRental_Backend
    ```

2. **Configure the Environment**
    - Create a `.env` file or set environment variables as per the [Configuration](#configuration) section.

3. **Install Dependencies**
    ```bash
    dotnet restore
    ```

4. **Database Migrations**
    ```bash
    dotnet ef database update
    ```

5. **Run the Application**
    ```bash
    dotnet run
    ```

6. **Access Swagger**
    - Open your browser and navigate to `https://localhost:<port>/swagger` to test the API.

---

*Thank you for your interest in the CarRental_Backend project!*