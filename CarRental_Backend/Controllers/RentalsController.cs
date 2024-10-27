using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CarRental_Backend.Data;
using CarRental_Backend.Models;

namespace CarRental_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get Car
            var car = await _context.Cars.FindAsync(model.Car_id);
            if (car == null)
                return NotFound("Car not found!");

            if (!car.IsFree)
                return BadRequest("Car is not avilable now.");

            // Get logged in user
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Create new rental
            var rental = new Rentals
            {
                Car_id = car.Car_id,
                Client_id = userId,
                Rental_date = model.Rental_date,
                Return_date = model.Return_date,
                Rental_price = 0, // Will be calculated later
                Discount = 0,
                IsReturned = false
            };

            // Set car as not free
            car.IsFree = false;
            _context.Rentals.Add(rental);

            // Calculate number of days
            int numberOfDays = (model.Return_date - model.Rental_date).Days;
            if (numberOfDays <= 0)
                return BadRequest("Return date must be later than rent date.");

            // Calculate rental price
            rental.Rental_price = CalculateRentalPrice(car.Car_PricePerDay, numberOfDays);

            // Save changes
            await _context.SaveChangesAsync();

            return Ok(rental);
        }


        // GET: api/Rentals
        [HttpPut("{id}/ConfirmReturn")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> ConfirmReturn(int id)
        {
            var rental = await _context.Rentals.Include(r => r.Car).FirstOrDefaultAsync(r => r.Rental_id == id);
            if (rental == null)
                return NotFound("Cloud not find rental.");

            if (rental.IsReturned)
                return BadRequest("Car has arleady been returned.");

            // Actual return date
            rental.IsReturned = true;
            rental.Return_date_actual = DateTime.Now;

            // Set car as free
            rental.Car.IsFree = true;

            await _context.SaveChangesAsync();

            return Ok("Car return is approved.");
        }


        // Calculate rental price method
        private decimal CalculateRentalPrice(decimal pricePerDay, int numberOfDays, decimal individualDiscount = 0)
        {
            decimal basePrice = pricePerDay * numberOfDays;
            decimal discountPercentage = 0;

            if (numberOfDays > 7)
            {
                discountPercentage = 0.40m;
            }
            else if (numberOfDays > 4)
            {
                discountPercentage = 0.20m;
            }
            else if (numberOfDays > 2)
            {
                discountPercentage = 0.10m;
            }

            // Individual discount
            discountPercentage += individualDiscount;

            if (discountPercentage > 0.5m)
            {
                discountPercentage = 0.5m; // Max discount is 50%
            }

            decimal discountAmount = basePrice * discountPercentage;
            return basePrice - discountAmount;
        }


        // GET: api/Rentals
        [HttpPut("{id}/ApplyDiscount")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> ApplyDiscount(int id, [FromBody] decimal discount)
        {
            var rental = await _context.Rentals.Include(r => r.Car).FirstOrDefaultAsync(r => r.Rental_id == id);
            if (rental == null)
                return NotFound("Rental not found!");

            if (discount < 0 || discount > 0.5m)
                return BadRequest("Discount has to be between 0% and 50%.");

            rental.Discount += discount;

            // Oblicz liczbę dni wynajmu
            int numberOfDays = (rental.Return_date - rental.Rental_date).Days;

            // Ponowne obliczenie ceny
            rental.Rental_price = CalculateRentalPrice(rental.Car.Car_PricePerDay, numberOfDays, rental.Discount);

            await _context.SaveChangesAsync();

            return Ok("Discount granted");
        }


        // GET: api/Rentals/MyRentals
        [HttpGet("MyRentals")]
        [Authorize]
        public async Task<IActionResult> GetMyRentals()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Where(r => r.Client_id == userId)
                .ToListAsync();

            return Ok(rentals);
        }


        // GET: api/Rentals/AllRentals
        [HttpGet]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _context.Rentals.Include(r => r.Car).Include(r => r.Client).ToListAsync();
            return Ok(rentals);
        }

    }
}
