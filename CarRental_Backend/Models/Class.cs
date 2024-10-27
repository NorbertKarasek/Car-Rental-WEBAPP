using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class CreateRentalModel
    {
        [Required]
        public int Car_id { get; set; }

        [Required]
        public DateTime Rental_date { get; set; }

        [Required]
        public DateTime Return_date { get; set; }
    }

}
