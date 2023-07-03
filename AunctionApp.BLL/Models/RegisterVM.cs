using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class RegisterVM
    {
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        [Required, MaxLength(50)]
        public string? Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [Required, DataType(DataType.PhoneNumber), MaxLength(12)]
        public string? PhoneNumber { get; set; }

        [Required, DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain at least one letter and one digit.")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "The password must be at least 6 characters long and contain at least one letter and one digit.")]
        public string ConfirmPassword { get; set; }

    }
}
