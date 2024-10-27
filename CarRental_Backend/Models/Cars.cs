using System.ComponentModel.DataAnnotations;

public class Cars
{
    [Key]
    public int Car_id { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Mileage { get; set; }
    [Required]
    public bool IsAutomatic { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public decimal PricePerDay { get; set; }
    [Required]
    public bool IsFree { get; set; } = true;
    [Required]
    public string Class { get; set; }
    public string Description { get; set; }

    // Collection of rentals
    public ICollection<Rentals> Rentals { get; set; } 
}
