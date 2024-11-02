using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CarRental_Backend.Models;
using System.Threading.Tasks;
using CarRental_Backend.Services;
using CarRental_Backend.Data;

namespace CarRental_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, TokenService tokenService)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if role is valid
            if (model.Role != "Client" && model.Role != "Employee")
            {
                return BadRequest("Invalid role. Role must be 'Client' or 'Employee'.");
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName
                // Different properties
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(user, model.Role);

            if (model.Role == "Client")
            {
                // Create Record in Client table
                var client = new Client
                {
                    ClientId = user.Id, // Use UserId as ClientId
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Surname = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    ApplicationUserId = user.Id
                    // Set other required properties
                };

                _context.Client.Add(client);
            }
            else if (model.Role == "Employee")
            {
                // Create record in Employee table
                var employee = new Employee
                {
                    EmployeeId = user.Id, // Use UserId as EmployeeId
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Surname = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    ApplicationUserId = user.Id
                    // Set other required properties
                };

                _context.Employee.Add(employee);
            }

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, "User created successfully!");

        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _tokenService.GenerateJwtToken(user);
                return Ok(new { token });
            }

            return Unauthorized("Invalid login attempt.");
        }
    }
}
