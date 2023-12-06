using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class ProfileImageVM
    {
        [DataType(DataType.ImageUrl),Required(ErrorMessage = "Please select a profile image.")]
        public IFormFile ProfileImagePath { get; set; }
    }
}
