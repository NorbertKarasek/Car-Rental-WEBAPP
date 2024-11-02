using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace CarRental_Backend.Models
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseIssueDate { get; set; }

        // Collection of Rental
        [JsonIgnore]
        public ICollection<Rental> Rental { get; set; }

        // Relation 1:1 with ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}