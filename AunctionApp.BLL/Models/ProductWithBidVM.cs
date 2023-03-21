using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class ProductWithBidVM
    {
        public string ProductName { get; set; }

        public IEnumerable<BidVM> Bids { get; set; }
    }
}
