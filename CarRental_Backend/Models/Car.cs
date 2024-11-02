using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Car
{
    [Key]
    public int CarId { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    [Range(1886, int.MaxValue, ErrorMessage = "Year must be valid.")]
    public int Year { get; set; }
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Mileage must be positive.")]
    public int Mileage { get; set; }
    [Required]
    public bool IsAutomatic { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "PricePerDay must be positive.")]
    public decimal PricePerDay { get; set; }
    [Required]
    public bool IsFree { get; set; } = true;
    [Required]
    public string Class { get; set; }
    public string Description { get; set; }

    // Collection of Rental
    [JsonIgnore]
    public ICollection<Rental> Rental { get; set; }
}

