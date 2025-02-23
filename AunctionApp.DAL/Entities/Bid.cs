namespace AunctionApp.DAL.Entities
{
    public class Bid : BaseEntity
    {
        public string BidPrice { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
        public string Bidder { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
