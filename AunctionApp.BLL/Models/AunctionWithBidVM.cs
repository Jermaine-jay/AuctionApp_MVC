using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class AunctionWithBidVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ActualAmount { get; set; }
        public string? ProductImage { get; set; }
        public string Description { get; set; }
        public IEnumerable<BidVM>? Bids { get; set; }
    }
}
