using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class ProfileImageVM
    {
        [Required(ErrorMessage = "Please select a profile image.")]
        [DataType(DataType.ImageUrl)]
        //[RegularExpression(@"^.*\.(jpeg|jpg|png|JPG)$", ErrorMessage = "Only .jpeg, .jpg, and .png files are allowed.")]
        public IFormFile ProfileImagePath { get; set; }
    }
}
