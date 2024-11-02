using CarRental_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CarRental_Backend.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Administrator", "Employee", "Client" };

            foreach (var roleName in roleNames)
            {
                // Check if role exists
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    // Create the roles and seed them to the database
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Creating a super user who could maintain the web app
            var adminEmail = "norbert.karasek94@gmail.com";
            var adminPassword = "Admin123!Admin123$"; // Ensure the password meets the requirements

            // Check if the admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // Create a new admin user
                var newAdminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Norbert",
                    LastName = "Karasek",
                    PhoneNumber = "123456789"
                };

                var result = await userManager.CreateAsync(newAdminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign role to user
                    await userManager.AddToRoleAsync(newAdminUser, "Administrator");
                }
                else
                {
                    // Error handling
                    throw new Exception("Cloud not create an admin account: " + string.Join(", ", result.Errors));
                }
            }
        }
    }
}
