using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CarRental_Backend.Models;
using CarRental_Backend.DTO;
using CarRental_Backend.Data;

namespace CarRental_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rental/RentACar
        [HttpPost("RentACar")]
        [Authorize]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get Car
            var car = await _context.Car.FindAsync(model.CarId);
            if (car == null)
                return NotFound("Car not found!");

            if (!car.IsFree)
                return BadRequest("Car is not available now.");

            // Get logged in user
            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));
            var client = await _context.Client.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (client == null)
                return NotFound("Client not found");

            // Create new rental
            var employeeWithLeastRental = await _context.Employee
                .OrderBy(e => _context.Rental.Count(r => r.EmployeeId == e.EmployeeId))
                .FirstOrDefaultAsync();

            if (employeeWithLeastRental == null)
                return NotFound("No Employee found.");

            var rental = new Rental
            {
                CarId = car.CarId,
                ClientId = client.ClientId,
                RentalDate = model.RentalDate,
                ReturnDate = model.ReturnDate,
                RentalPrice = 0, // Will be calculated later
                Discount = 0,
                EmployeeId = employeeWithLeastRental.EmployeeId,
                IsReturned = false
            };

            // Set car as not free
            car.IsFree = false;
            _context.Rental.Add(rental);

            // Calculate number of days
            int numberOfDays = (model.ReturnDate - model.RentalDate).Days;
            if (numberOfDays <= 0)
                return BadRequest("Return date must be later than rent date.");

            // Calculate rental price
            rental.RentalPrice = CalculateRentalPrice(car.PricePerDay, numberOfDays);

            // Save changes
            await _context.SaveChangesAsync();

            // Map rental to DTO
            var rentalDTO = new RentalDTO
            {
                RentalId = rental.RentalId,
                CarId = rental.CarId,
                ClientId = rental.ClientId,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                RentalPrice = rental.RentalPrice,
                IsReturned = rental.IsReturned,

                // Add more fields if needed
            };

            return Ok(rentalDTO);
        }



        // GET: api/Rental/{id}/ConfirmReturn
        [HttpPut("{id}/ConfirmReturn")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> ConfirmReturn(int id)
        {
            var rental = await _context.Rental.Include(r => r.Car).FirstOrDefaultAsync(r => r.RentalId == id);
            if (rental == null)
                return NotFound("Cloud not find rental.");

            if (rental.IsReturned)
                return BadRequest("Car has arleady been returned.");

            // Actual return date
            rental.IsReturned = true;
            rental.ReturnDateActual = DateTime.Now;

            // Set car as free
            rental.Car.IsFree = true;

            // Calculate additional fees
            if (rental.ReturnDateActual > rental.ReturnDate)
            {
                int extraDays = (rental.ReturnDateActual.Value - rental.ReturnDate).Days;
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


        // GET: api/Rental/{id}/ApplyDiscount
        [HttpPut("{id}/ApplyDiscount")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> ApplyDiscount(int id, [FromBody] DiscountModel model)
        {
            var discount = model.Discount;

            var rental = await _context.Rental.Include(r => r.Car).FirstOrDefaultAsync(r => r.RentalId == id);
            if (rental == null)
                return NotFound("Rental not found!");

            if (discount < 0 || discount > 0.5m)
                return BadRequest("Discount has to be between 0% and 50%.");

            rental.Discount += discount;

            // Count number of days again
            int numberOfDays = (rental.ReturnDate - rental.RentalDate).Days;

            // Count new rental price
            rental.RentalPrice = CalculateRentalPrice(rental.Car.PricePerDay, numberOfDays, rental.Discount);

            await _context.SaveChangesAsync();

            return Ok("Discount granted");
        }


        // GET: api/Rental/MyRental
        [HttpGet("MyRental")]
        [Authorize]
        public async Task<IActionResult> GetMyRental()
        {
            var userId = User.Claims
                .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Select(c => c.Value)
                .FirstOrDefault(value => Guid.TryParse(value, out _));

            // Check if the user is a client
            var client = await _context.Client.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (client != null)
            {
                var Rental = await _context.Rental
                    .Include(r => r.Car)
                    .Where(r => r.ClientId == client.ClientId)
                    .Select(r => new RentalDTO
                    {
                        RentalId = r.RentalId,
                        RentalDate = r.RentalDate,
                        ReturnDate = r.ReturnDate,
                        RentalPrice = r.RentalPrice,
                        Discount = r.Discount,
                        AdditionalFees = r.AdditionalFees,
                        IsReturned = r.IsReturned,
                        ReturnDateActual = r.ReturnDateActual,
                        Car = new CarDTO
                        {
                            CarId = r.Car.CarId,
                            Brand = r.Car.Brand,
                            Model = r.Car.Model,
                            // add other needed fields
                        }
                        // You can provide ClientDTO if it is not needed
                    })
                    .ToListAsync();

                return Ok(Rental);
            }

            // Check if the user is an employee
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.ApplicationUserId == userId);
            if (employee != null)
            {
                var Rental = await _context.Rental
                    .Include(r => r.Car)
                    .Where(r => r.EmployeeId == employee.EmployeeId)
                    .Select(r => new RentalDTO
                    {
                        RentalId = r.RentalId,
                        RentalDate = r.RentalDate,
                        ReturnDate = r.ReturnDate,
                        RentalPrice = r.RentalPrice,
                        Discount = r.Discount,
                        AdditionalFees = r.AdditionalFees,
                        IsReturned = r.IsReturned,
                        ReturnDateActual = r.ReturnDateActual,
                        Car = new CarDTO
                        {
                            CarId = r.Car.CarId,
                            Brand = r.Car.Brand,
                            Model = r.Car.Model,
                            // add other needed fields
                        }
                        // You can provide ClientDTO if it is not needed
                    })
                    .ToListAsync();

                return Ok(Rental);
            }

            return NotFound("User not found.");
        }




        // GET: api/Rental/AllRental
        [HttpGet("AllRental")]
        [Authorize(Roles = "Employee,Administrator")]
        public async Task<IActionResult> GetAllRental()
        {
            var Rental = await _context.Rental
                .Include(r => r.Car)
                .Include(r => r.Client)
                .Select(r => new RentalDTO
                {
                    RentalId = r.RentalId,
                    RentalDate = r.RentalDate,
                    ReturnDate = r.ReturnDate,
                    RentalPrice = r.RentalPrice,
                    Discount = r.Discount,
                    AdditionalFees = r.AdditionalFees,
                    IsReturned = r.IsReturned,
                    ReturnDateActual = r.ReturnDateActual,
                    Car = new CarDTO
                    {
                        CarId = r.Car.CarId,
                        Brand = r.Car.Brand,
                        Model = r.Car.Model,
                        // Add other needed fields
                    },
                    Client = new ClientDTO
                    {
                        ClientId = r.Client.ClientId,
                        FirstName = r.Client.FirstName,
                        Surname = r.Client.Surname,
                        Email = r.Client.Email,
                        PhoneNumber = r.Client.PhoneNumber,
                        // Add other needed fields
                    }
                })
                .ToListAsync();

            return Ok(Rental);
        }

    }
}
