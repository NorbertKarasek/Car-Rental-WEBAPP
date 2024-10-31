using CarRental_Backend.Data;
using CarRental_Backend.DTO;
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

        // PUT: api/Clients/MyProfile
        [Authorize(Roles = "Client")]
        [HttpPut("MyProfile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateClientProfileDto updatedClientDto)
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

            // Update client properties with new values
            client.FirstName = updatedClientDto.FirstName;
            client.Surname = updatedClientDto.Surname;
            client.PhoneNumber = updatedClientDto.PhoneNumber;
            client.Address = updatedClientDto.Address;
            client.City = updatedClientDto.City;
            client.Country = updatedClientDto.Country;
            client.DateOfBirth = updatedClientDto.DateOfBirth;
            client.LicenseNumber = updatedClientDto.LicenseNumber;
            client.LicenseIssueDate = updatedClientDto.LicenseIssueDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
