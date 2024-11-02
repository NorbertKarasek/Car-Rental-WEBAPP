using System.ComponentModel.DataAnnotations;

namespace CarRental_Backend.Models
{
    public class CreateRentalModel
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        [DateGreaterThan("RentalDate", ErrorMessage = "ReturnDate must be later than RentalDate.")]
        public DateTime ReturnDate { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
