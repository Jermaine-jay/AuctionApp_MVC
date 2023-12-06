using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class AuctionVM
    {
        public string? Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ActualAmount { get; set; }

        [DataType(DataType.ImageUrl), Required(ErrorMessage = "Please select a profile image.")]
        public IFormFile ProductImagePath { get; set; }
        public string? IsSold { get; set; }
    }
}
