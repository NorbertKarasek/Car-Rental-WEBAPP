using CarRental_Backend.Data;
using CarRental_Backend.Extensions;
using CarRental_Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();

builder.Services.AddControllers();

// Build the app.
var app = builder.Build();

// Initialize the database and roles.
await app.SeedDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

// Use Authentication
app.UseAuthentication();

// Use Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
