using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class AuctionVMForm
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ActualAmount { get; set; }
        public string ProductImagePath { get; set; }
        public string Status { get; set; }
    }
}
