using Microsoft.AspNetCore.Http;

namespace AunctionApp.BLL.Models
{
    public class ProfileImageVM
    {
        //public string? UserId { get; set; }
        public IFormFile ProfileImagePath { get; set; }
    }
}
