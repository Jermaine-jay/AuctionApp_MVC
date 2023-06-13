using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class AuctionVMForm
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }

        //[Required, StringLength(3000, ErrorMessage = "character limit of 3 and 1000 is exceeded", MinimumLength = 3)]
        public string Description { get; set; }
        public string ActualAmount { get; set; }
        public string ProductImagePath { get; set; }
        public string Status { get; set; }
    }
}
