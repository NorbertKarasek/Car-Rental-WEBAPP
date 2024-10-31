using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CarRental_Backend.Data;
using CarRental_Backend.Models;
using CarRental_Backend.DTO;

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

        // GET: api/Rentals/RentACar
        [HttpPost("RentACar")]
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
                return BadRequest("Car is not available now.");

            // Get logged in user
            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (client == null)
                return NotFound("Client not found");

            // Create new rental
            var employeeWithLeastRentals = await _context.Employees
                .OrderBy(e => _context.Rentals.Count(r => r.Employee_id == e.Employee_id))
                .FirstOrDefaultAsync();

            if (employeeWithLeastRentals == null)
                return NotFound("No employees found.");

            var rental = new Rentals
            {
                Car_id = car.Car_id,
                Client_id = client.Client_id,
                Rental_date = model.Rental_date,
                Return_date = model.Return_date,
                Rental_price = 0, // Will be calculated later
                Discount = 0,
                Employee_id = employeeWithLeastRentals.Employee_id,
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
            rental.Rental_price = CalculateRentalPrice(car.PricePerDay, numberOfDays);

            // Save changes
            await _context.SaveChangesAsync();

            // Map rental to DTO
            var rentalDTO = new RentalDTO
            {
                Rental_id = rental.Rental_id,
                Car_id = rental.Car_id,
                Client_id = rental.Client_id,
                Rental_date = rental.Rental_date,
                Return_date = rental.Return_date,
                Rental_price = rental.Rental_price,
                IsReturned = rental.IsReturned,

                // Add more fields if needed
            };

            return Ok(rentalDTO);
        }



        // GET: api/Rentals/{id}/ConfirmReturn
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

            // Calculate additional fees
            if (rental.Return_date_actual > rental.Return_date)
            {
                int extraDays = (rental.Return_date_actual.Value - rental.Return_date).Days;
                decimal extraFees = extraDays * rental.Car.PricePerDay;
                rental.AdditionalFees = extraFees;
            }

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


        // GET: api/Rentals/{id}/ApplyDiscount
        [HttpPut("{id}/ApplyDiscount")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> ApplyDiscount(int id, [FromBody] DiscountModel model)
        {
            var discount = model.Discount;

            var rental = await _context.Rentals.Include(r => r.Car).FirstOrDefaultAsync(r => r.Rental_id == id);
            if (rental == null)
                return NotFound("Rental not found!");

            if (discount < 0 || discount > 0.5m)
                return BadRequest("Discount has to be between 0% and 50%.");

            rental.Discount += discount;

            // Count number of days again
            int numberOfDays = (rental.Return_date - rental.Rental_date).Days;

            // Count new rental price
            rental.Rental_price = CalculateRentalPrice(rental.Car.PricePerDay, numberOfDays, rental.Discount);

            await _context.SaveChangesAsync();

            return Ok("Discount granted");
        }


        // GET: api/Rentals/MyRentals
        [HttpGet("MyRentals")]
        [Authorize]
        public async Task<IActionResult> GetMyRentals()
        {
            var userId = User.Claims
                .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Select(c => c.Value)
                .FirstOrDefault(value => Guid.TryParse(value, out _));

            // Check if the user is a client
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (client != null)
            {
                var rentals = await _context.Rentals
                    .Include(r => r.Car)
                    .Where(r => r.Client_id == client.Client_id)
                    .Select(r => new RentalDTO
                    {
                        Rental_id = r.Rental_id,
                        Rental_date = r.Rental_date,
                        Return_date = r.Return_date,
                        Rental_price = r.Rental_price,
                        Discount = r.Discount,
                        AdditionalFees = r.AdditionalFees,
                        IsReturned = r.IsReturned,
                        Return_date_actual = r.Return_date_actual,
                        Car = new CarDTO
                        {
                            Car_id = r.Car.Car_id,
                            Brand = r.Car.Brand,
                            Model = r.Car.Model,
                            // add other needed fields
                        }
                        // You can provide ClientDTO if it is not needed
                    })
                    .ToListAsync();

                return Ok(rentals);
            }

            // Check if the user is an employee
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ApplicationUserId == userId);
            if (employee != null)
            {
                var rentals = await _context.Rentals
                    .Include(r => r.Car)
                    .Where(r => r.Employee_id == employee.Employee_id)
                    .Select(r => new RentalDTO
                    {
                        Rental_id = r.Rental_id,
                        Rental_date = r.Rental_date,
                        Return_date = r.Return_date,
                        Rental_price = r.Rental_price,
                        Discount = r.Discount,
                        AdditionalFees = r.AdditionalFees,
                        IsReturned = r.IsReturned,
                        Return_date_actual = r.Return_date_actual,
                        Car = new CarDTO
                        {
                            Car_id = r.Car.Car_id,
                            Brand = r.Car.Brand,
                            Model = r.Car.Model,
                            // add other needed fields
                        }
                        // You can provide ClientDTO if it is not needed
                    })
                    .ToListAsync();

                return Ok(rentals);
            }

            return NotFound("User not found.");
        }




        // GET: api/Rentals/AllRentals
        [HttpGet("AllRentals")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Client)
                .Select(r => new RentalDTO
                {
                    Rental_id = r.Rental_id,
                    Rental_date = r.Rental_date,
                    Return_date = r.Return_date,
                    Rental_price = r.Rental_price,
                    Discount = r.Discount,
                    AdditionalFees = r.AdditionalFees,
                    IsReturned = r.IsReturned,
                    Return_date_actual = r.Return_date_actual,
                    Car = new CarDTO
                    {
                        Car_id = r.Car.Car_id,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        // Add other needed fields
                    },
                    Client = new ClientDTO
                    {
                        Client_id = r.Client.Client_id,
                        FirstName = r.Client.FirstName,
                        Surname = r.Client.Surname,
                        Email = r.Client.Email,
                        PhoneNumber = r.Client.PhoneNumber,
                        // Add other needed fields
                    }
                })
                .ToListAsync();

            return Ok(rentals);
        }

    }
}
