using CarRental_Backend.Models;
using System.ComponentModel.DataAnnotations;

public class Rentals
{
    [Key]
    public int Rental_id { get; set; }

    public int Car_id { get; set; }
    public Cars Car { get; set; }

    public int Client_id { get; set; }
    public Clients Client { get; set; }

    public DateTime Rental_date { get; set; }
    public DateTime Return_date { get; set; }
    public decimal Rental_price { get; set; }
    public decimal Discount { get; set; }
    public decimal AdditionalFees { get; set; }
    public bool IsReturned { get; set; }
    public DateTime? Return_date_actual { get; set; }
    public int? Employee_id { get; set; }
    public Employees Employee { get; set; }
}
