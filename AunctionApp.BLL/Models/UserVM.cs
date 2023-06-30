using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class UserVM
    {
        public string Id { get; set; }

        [Required, MaxLength(50)]
        public string? UserName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required, DataType(DataType.PhoneNumber), MaxLength(12)]
        public string PhoneNumber { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
