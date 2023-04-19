using AunctionApp.DAL.Entities;

namespace AunctionApp.BLL.Models
{
    public class AddOrUpdateBidVM
    {
        public int Id { get; set; }
        public string BidPrice { get; set; }
        public string BidderUsername { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
