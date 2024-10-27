using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Employees
    {
        [Key]
        public int Employee_id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string Emplotyee_Name { get; set; }
        [Required]
        public string Emplotyee_Surname { get; set; }
        [Required]
        public string Employee_Email { get; set; }
        [Required]
        public string Emplotyee_Phone { get; set; }
        [Required]
        public string Emplotyee_Address { get; set; }
        [Required]
        public string Emplotyee_City { get; set; }
        [Required]
        public string Emplotyee_Country { get; set; }
        [Required]
        public DateTime Emplotyee_Date_of_birth { get; set; }
        [Required]
        public decimal Emplotyee_Salary { get; set; }
        [Required]
        public string Emplotyee_Position { get; set; }
    }

}
