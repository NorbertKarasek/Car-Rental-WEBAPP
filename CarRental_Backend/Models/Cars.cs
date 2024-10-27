using System.ComponentModel.DataAnnotations;

public class Cars
{
    [Key]
    public int Car_id { get; set; }
    [Required]
    public string Car_Brand { get; set; }
    [Required]
    public string Car_Model { get; set; }
    [Required]
    public string Car_Type { get; set; }
    [Required]
    public int Car_Year { get; set; }
    [Required]
    public int Car_Mileage { get; set; }
    [Required]
    public bool Car_Gear_is_automatic { get; set; }
    [Required]
    public string Car_Color { get; set; }
    [Required]
    public decimal Car_PricePerDay { get; set; }
    [Required]
    public bool IsFree { get; set; } = true;
    [Required]
    public string Car_class { get; set; }
    public string Description { get; set; }

    // Collection of rentals
    public ICollection<Rentals> Rentals { get; set; } 
}
