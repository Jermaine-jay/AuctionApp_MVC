namespace AunctionApp.DAL.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ActualAmount { get; set; }
        public string ProductImagePath { get; set; }
        public bool IsSold { get; set; }
        public ICollection<Bid>? BidList { get; set; }
    }
}
