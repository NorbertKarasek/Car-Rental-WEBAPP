using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class Cars
    {
        [Key]
        public int Car_id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public bool Gear_is_automatic { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public bool Free { get; set; }
    }

}
