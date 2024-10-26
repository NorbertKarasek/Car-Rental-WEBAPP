using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Clients
    {
        [Key]
        public int Client_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
    }

}
