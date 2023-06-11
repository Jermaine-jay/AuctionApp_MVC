using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class RegisterVM
    {
        public string? Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string OtherName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required, DataType(DataType.PhoneNumber), MaxLength(12)]
        public string PhoneNumber { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
