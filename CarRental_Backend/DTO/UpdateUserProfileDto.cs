namespace CarRental_Backend.DTO
{
    public class UpdateUserProfileDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class UpdateClientProfileDto : UpdateUserProfileDto
    {
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseIssueDate { get; set; }
    }

    public class UpdateEmployeeProfileDto : UpdateUserProfileDto
    {
        public string? Position { get; set; }
    }

}
