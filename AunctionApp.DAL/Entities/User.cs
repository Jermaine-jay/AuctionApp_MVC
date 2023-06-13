using Microsoft.AspNetCore.Identity;

namespace AunctionApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string ProfileImagePath { get; set; } = "Blank-Pfp.jpg";

    }
}
