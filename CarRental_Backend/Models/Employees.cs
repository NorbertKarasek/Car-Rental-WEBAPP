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
        public string Employee_Name { get; set; }
        [Required]
        public string Employee_Surname { get; set; }
        [Required]
        public string Employee_Email { get; set; }
        [Required]
        public string Employee_Phone { get; set; }
        [Required]
        public string Employee_Address { get; set; }
        [Required]
        public string Employee_City { get; set; }
        [Required]
        public string Employee_Country { get; set; }
        [Required]
        public DateTime Employee_Date_of_birth { get; set; }
        [Required]
        public decimal Employee_Salary { get; set; }
        [Required]
        public string Employee_Position { get; set; }
    }

}
