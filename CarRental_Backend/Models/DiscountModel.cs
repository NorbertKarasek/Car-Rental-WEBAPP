using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class DiscountModel
    {
        [Range(0, 0.5, ErrorMessage = "Discount must be between 0% and 50%.")]
        public decimal Discount { get; set; }
    }

}
