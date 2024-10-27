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
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }


        // GET: api/Client/index
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetClient(int id)
        {
            var client = await _context.Cars.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }
    }
}
