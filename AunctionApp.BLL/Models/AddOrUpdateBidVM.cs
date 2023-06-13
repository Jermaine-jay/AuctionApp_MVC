using AunctionApp.DAL.Entities;

namespace AunctionApp.BLL.Models
{
    public class AddOrUpdateBidVM
    {
        public int? ProductId { get; set; }
        public string? BidPrice { get; set; }
        public string Bidder { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
    }
}
