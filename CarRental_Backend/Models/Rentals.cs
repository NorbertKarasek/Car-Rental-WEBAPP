using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Rentals
    {
        [Key]
        public int Rental_id { get; set; }
        public int Car_id { get; set; }
        public int Client_id { get; set; }
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
        public int Rental_price { get; set; }
    }

}
