using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Employees
    {
        [Key]
        [StringLength(255)]
        public string Employee_id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Position { get; set; }
    }

}
