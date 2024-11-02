using CarRental_Backend.Models;
using System.ComponentModel.DataAnnotations;

public class Rental
{
    [Key]
    public int RentalId { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; }

    public string ClientId { get; set; }
    public Client Client { get; set; }

    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal RentalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal AdditionalFees { get; set; }
    public bool IsReturned { get; set; }
    public DateTime? ReturnDateActual { get; set; }
    [StringLength(255)]
    public string EmployeeId { get; set; }
    public Employee Employee { get; set; }
}

