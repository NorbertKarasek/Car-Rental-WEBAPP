﻿using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Employees
    {
        [Key]
        [StringLength(255)]
        public string Employee_id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Salary { get; set; }
        public string? Position { get; set; }
    }
}
