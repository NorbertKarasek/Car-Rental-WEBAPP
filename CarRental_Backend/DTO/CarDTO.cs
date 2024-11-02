namespace CarRental_Backend.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public bool IsAutomatic { get; set; }
        public string Color { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsFree { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
    }


}
