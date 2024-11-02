using CarRental_Backend.Data.Configuration;
using CarRental_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            return await _context.Car.ToListAsync();
        }


        // GET: api/Car/index
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
    }
}
