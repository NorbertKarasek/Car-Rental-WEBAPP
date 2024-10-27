using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CarRental_Backend.Models
{
    public class Clients
    {
        [Key]
        public int Client_id { get; set; }
        [Required]
        public string Client_Name { get; set; }
        [Required]
        public string Client_Surname { get; set; }
        [Required]
        public string Client_Email { get; set; }
        [Required]
        public string Client_Phone { get; set; }
        public string Client_Address { get; set; }
        public string Client_City { get; set; }
        public string Client_Country { get; set; }
        public DateTime? Client_Date_of_birth { get; set; }
        public string License_number { get; set; }
        public DateTime? License_issue_date { get; set; }

        // Collection of rentals
        public ICollection<Rentals> Rentals { get; set; } 

        // Relation 1:1 with ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

}