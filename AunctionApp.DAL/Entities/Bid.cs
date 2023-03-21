using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.DAL.Entities
{
    public class Bid : BaseEntity
    {
        public string BidPrice { get; set; }
        public DateTime BidTime { get; set; } = DateTime.Now;
        public string BidderFirstName { get; set; }
        public string BidderLastName { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
