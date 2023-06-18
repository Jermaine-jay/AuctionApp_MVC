using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class ProfileImageVM
    {
        public string? UserId { get; set; }
        //[RegularExpression(@"^.*\.(jpeg|jpg|png)$", ErrorMessage = "Only .jpeg, .jpg, and .png files are allowed.")]
        public IFormFile ProfileImagePath { get; set; }
    }
}
