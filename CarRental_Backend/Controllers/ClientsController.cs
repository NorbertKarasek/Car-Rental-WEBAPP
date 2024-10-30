using CarRental_Backend.Data;
using CarRental_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarRental_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(ApplicationDbContext context, ILogger<ClientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Clients
        [Authorize(Roles = "Employee,Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/ById/{id}
        [Authorize(Roles = "Employee,Administrator")]
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Clients>> GetClient(string id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // GET: api/Clients/MyProfile
        [Authorize(Roles = "Client")]
        [HttpGet("MyProfile")]
        public async Task<ActionResult<Clients>> GetMyProfile()
        {

            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/MyProfileChanges
        [Authorize(Roles = "Client")]
        [HttpPut("MyProfile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] Clients updatedClient)
        {
            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));

            _logger.LogInformation("User ID (nameidentifier as GUID): {UserId}", userId);

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (client == null)
            {
                return NotFound();
            }

            // Update client properties with new values
            client.FirstName = updatedClient.FirstName;
            client.Surname = updatedClient.Surname;
            client.PhoneNumber = updatedClient.PhoneNumber;
            client.Address = updatedClient.Address;
            client.City = updatedClient.City;
            client.Country = updatedClient.Country;
            client.DateOfBirth = updatedClient.DateOfBirth;
            client.LicenseNumber = updatedClient.LicenseNumber;
            client.LicenseIssueDate = updatedClient.LicenseIssueDate;

            // Dont let user change email and applicationUserId

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
