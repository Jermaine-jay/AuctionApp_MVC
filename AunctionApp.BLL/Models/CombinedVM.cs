using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class CombinedVM
    {
        public AddOrUpdateBidVM Bid { get; set; }
        public IEnumerable<AuctionWithBidVM> Aunction { get; set; }
    }
}
