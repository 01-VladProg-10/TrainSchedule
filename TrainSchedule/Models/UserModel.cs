using System;
using System.ComponentModel.DataAnnotations;

namespace TrainSchedule.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be 9 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ticket Type is required.")]
        public string TicketType { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;

        // Metoda do walidacji danych
        public bool Validate(out string errorMessage)
        {
            var context = new ValidationContext(this);
            var results = new System.Collections.Generic.List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                errorMessage = string.Join("\n", results.ConvertAll(r => r.ErrorMessage));
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
