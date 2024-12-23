﻿namespace CarRental_Backend.DTO
{
    public class ClientDTO
    {
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseIssueDate { get; set; }
    }


}
