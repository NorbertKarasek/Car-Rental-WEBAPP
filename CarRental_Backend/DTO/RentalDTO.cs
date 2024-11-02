namespace CarRental_Backend.DTO
{
    public class RentalDTO
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public CarDTO Car { get; set; } // Opcjonalne
        public string ClientId { get; set; }
        public ClientDTO Client { get; set; } // Opcjonalne
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? AdditionalFees { get; set; }
        public bool IsReturned { get; set; }
        public DateTime? ReturnDateActual { get; set; }
        public string EmployeeId { get; set; }
    }

}
