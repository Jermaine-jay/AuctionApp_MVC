namespace AunctionApp.DAL.Entities
{
    public class Bid : BaseEntity
    {
        public string BidPrice { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
        public string Bidder { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
