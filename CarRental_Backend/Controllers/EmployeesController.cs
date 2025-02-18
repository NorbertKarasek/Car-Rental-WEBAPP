﻿using CarRental_Backend.Data;
using CarRental_Backend.DTO;
using CarRental_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CarRental_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }


        // GET: api/Employee/ById/{id}
        [Authorize]
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        // GET: api/Employee/MyProfile
        [Authorize(Roles = "Employee,Administrator")]
        [HttpGet("MyProfile")]
        public async Task<ActionResult<Employee>> GetMyProfile()
        {
            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));

            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.ApplicationUserId == userId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }


        // PUT: api/Employee/MyProfile
        [Authorize(Roles = "Employee,Administrator")]
        [HttpPut("MyProfile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateEmployeeProfileDto updatedEmployeeDto)
        {
            var userId = User.Claims
            .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            .Select(c => c.Value)
            .FirstOrDefault(value => Guid.TryParse(value, out _));
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.ApplicationUserId == userId);

            if (employee == null)
            {
                return NotFound();
            }

            // Update employee properties with new values
            employee.FirstName = updatedEmployeeDto.FirstName;
            employee.Surname = updatedEmployeeDto.Surname;
            employee.PhoneNumber = updatedEmployeeDto.PhoneNumber;
            employee.Address = updatedEmployeeDto.Address;
            employee.City = updatedEmployeeDto.City;
            employee.Country = updatedEmployeeDto.Country;
            employee.DateOfBirth = updatedEmployeeDto.DateOfBirth;
            employee.Position = updatedEmployeeDto.Position;

            // Dont allow to change salary by this method - only by administrator

            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
