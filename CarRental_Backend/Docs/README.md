```markdown
# Dokumentacja Projektu CarRental_Backend

## Spis Treści
- **Opis Projektu**
- **Technologie**
- **Struktura Projektu**
- **Konfiguracja**
  - **appsettings.json**
  - **Zmienne Środowiskowe**
- **Modele Danych**
- **Data Transfer Objects (DTO)**
- **Usługi (Services)**
- **Kontrolery**
- **Autentykacja i Autoryzacja**
- **Inicjalizacja Bazy Danych**
- **Konfiguracje Encji**
- **Rozszerzenia (Extensions)**
- **Środowisko Uruchomieniowe**
- **Dodatkowe Informacje**

## Opis Projektu
**CarRental_Backend** to aplikacja webowa umożliwiająca zarządzanie wypożyczalnią samochodów. Projekt oferuje funkcjonalności takie jak:

- Rejestracja i logowanie użytkowników (klientów i pracowników)
- Zarządzanie profilami użytkowników
- Przeglądanie dostępnych samochodów
- Rezerwacja i wynajem samochodów
- Zarządzanie wypożyczeniami
- Przyznawanie zniżek i naliczanie opłat dodatkowych
- Autentykacja i autoryzacja z użyciem JWT

## Technologie
- **.NET 6 (ASP.NET Core 6)**
- **Entity Framework Core 6**
- **MySQL Server**
- **Identity Framework**
- **JWT Authentication**
- **Swagger (Swashbuckle)**
- **C# 10**

## Struktura Projektu

CarRental_Backend/
├── Controllers/
│   └── (Kontrolery API)
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── DbInitializer.cs
│   └── Configurations/
│       └── (Konfiguracje encji)
├── DTO/
│   └── (Data Transfer Objects)
├── Extensions/
│   ├── ApplicationBuilderExtensions.cs
│   └── ServiceExtensions.cs
├── Models/
│   └── (Modele danych)
├── Services/
│   └── TokenService.cs
├── appsettings.json
└── Program.cs
```

## Konfiguracja

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
**Uwaga**: Poufne informacje, takie jak `User Id`, `Password` oraz `Key`, są przechowywane w zmiennych środowiskowych.

### Zmienne Środowiskowe
- `ConnectionStrings__DefaultConnection`: Pełny connection string do bazy danych, zawierający `User Id` i `Password`.
- `JwtSettings__Key`: Sekretny klucz używany do generowania tokenów JWT.

## Modele Danych

### `ApplicationUser`
Reprezentuje użytkownika systemu.

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
Reprezentuje klienta.

```csharp
public class Client
{
    [Key]
    public string ClientId { get; set; }
    [Required]
    public string FirstName { get; set; }
    // ... pozostałe właściwości
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
```

### `Employee`
Reprezentuje pracownika.

```csharp
public class Employee
{
    [Key]
    public string EmployeeId { get; set; }
    [Required]
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    // ... pozostałe właściwości
}
```

### `Car`
Reprezentuje samochód.

```csharp
public class Car
{
    [Key]
    public int CarId { get; set; }
    [Required]
    public string Brand { get; set; }
    // ... pozostałe właściwości
    public ICollection<Rental> Rentals { get; set; }
}
```

### `Rental`
Reprezentuje wypożyczenie.

```csharp
public class Rental
{
    [Key]
    public int RentalId { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public string ClientId { get; set; }
    public Client Client { get; set; }
    // ... pozostałe właściwości
}
```

## Data Transfer Objects (DTO)

### `CarDTO`

```csharp
public class CarDTO
{
    public int CarId { get; set; }
    public string Brand { get; set; }
    // ... pozostałe właściwości
}
```

### `ClientDTO`

```csharp
public class ClientDTO
{
    public string ClientId { get; set; }
    public string FirstName { get; set; }
    // ... pozostałe właściwości
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
    // ... pozostałe właściwości
}
```

## Usługi (Services)

### `TokenService.cs`
Usługa odpowiedzialna za generowanie tokenów JWT.

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
        // Implementacja generowania tokenu JWT
    }
}
```

## Kontrolery
Kontrolery API obsługują żądania HTTP i zarządzają logiką biznesową:

- **AuthController**: Rejestracja i logowanie użytkowników.
- **CarsController**: Zarządzanie samochodami.
- **RentalsController**: Zarządzanie wypożyczeniami.
- **ClientsController**: Zarządzanie profilami klientów.
- **EmployeesController**: Zarządzanie profilami pracowników.

## Autentykacja i Autoryzacja
- **Autentykacja**: JWT; token JWT musi być dołączany do kolejnych żądań w nagłówku `Authorization: Bearer {token}`.
- **Autoryzacja**: Role (Administrator, Employee, Client) kontrolują dostęp do zasobów.

## Inicjalizacja Bazy Danych
Plik `DbInitializer.cs` odpowiedzialny jest za:

- Tworzenie ról w systemie (Administrator, Employee, Client).
- Tworzenie konta administratora z domyślnymi danymi.
- Dodawanie początkowych danych do bazy (jeśli wymagane).

```csharp
public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // Implementacja inicjalizacji bazy danych
    }
}
```

## Konfiguracje Encji
Znajdują się w katalogu `Data/Configurations/` i zawierają konfiguracje encji dla Entity Framework Core.

### Przykład: RentalsConfiguration.cs

```csharp
public class RentalsConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        // Konfiguracja właściwości i relacji encji Rental
    }
}
```

## Rozszerzenia (Extensions)

### `ServiceExtensions.cs`
Zawiera metody rozszerzające do konfiguracji usług:

- **ConfigureDatabase**: Konfiguracja bazy danych.
- **ConfigureIdentity**: Konfiguracja Identity Framework.
- **ConfigureJWT**: Konfiguracja autentykacji JWT.
- **ConfigureServices**: Rejestracja własnych usług.
- **ConfigureCors**: Konfiguracja CORS.
- **ConfigureSwagger**: Konfiguracja Swaggera.

### `ApplicationBuilderExtensions.cs`
Zawiera metody rozszerzające do konfiguracji aplikacji.

- **SeedDatabaseAsync**: Inicjalizacja bazy danych podczas uruchamiania aplikacji.

## Środowisko Uruchomieniowe
Plik `Program.cs` jest głównym punktem wejścia aplikacji.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Dodanie zmiennych środowiskowych
builder.Configuration

.AddEnvironmentVariables();

// Konfiguracja usług
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();

// Budowa aplikacji
var app = builder.Build();

// Inicjalizacja bazy danych i ról
await app.SeedDatabaseAsync();

// Konfiguracja middleware
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

## Dodatkowe Informacje

- **Migracje Bazy Danych**: Należy uruchomić migracje, aby zaktualizować strukturę bazy danych zgodnie z modelami.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- **Bezpieczeństwo**: Poufne dane są przechowywane w zmiennych środowiskowych lub w narzędziach takich jak User Secrets podczas developmentu.
- **Swagger**: Dostępny pod `/swagger` w środowisku deweloperskim, umożliwia testowanie API.
- **Konfiguracja Haseł**: Wymagania dotyczące haseł można dostosować w metodzie `ConfigureIdentity` w `ServiceExtensions.cs`.
- **Obsługa Błędów**: Warto zaimplementować globalną obsługę błędów i logowanie.

**Autor**: Twój zespół deweloperski

**Data Aktualizacji**: [Data]

**Uwagi Końcowe**

- Dokumentacja powinna być aktualizowana wraz ze zmianami w projekcie.
- Rozważ dodanie szczegółów dotyczących implementacji kontrolerów, usług oraz przykładów użycia API.
- Zadbaj o odpowiednie zabezpieczenia przed udostępnieniem aplikacji w środowisku produkcyjnym.
```