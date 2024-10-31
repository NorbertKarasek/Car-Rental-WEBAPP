namespace CarRental_Backend.DTO
{
    public class RentalDTO
    {
        public int Rental_id { get; set; }
        public int Car_id { get; set; }
        public CarDTO Car { get; set; } // For displaying car details
        public string Client_id { get; set; }
        public ClientDTO Client { get; set; } // For displaying client details
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
        public decimal Rental_price { get; set; }
        public decimal Discount { get; set; }
        public decimal AdditionalFees { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? Return_date_actual { get; set; }
        public string Employee_id { get; set; }
    }


}
