namespace CarRental_Backend.DTO
{
    public class UpdateEmployeeProfileDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Position { get; set; }
    }
}
