using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class AunctionVM
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ActualAmount { get; set; }

        [Display(Name = "Image")]
        public IFormFile ProductImage { get; set; }
        public string IsSold { get; set; }
    }
}
