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

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [Authorize(Roles = "Employee,Administrator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/{id}
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
