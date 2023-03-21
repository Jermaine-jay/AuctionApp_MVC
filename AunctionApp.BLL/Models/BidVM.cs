using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class BidVM
    {
        public string BidPrice { get; set; }
        public DateTime BidTime { get; set; }
        public string BidderFirstName { get; set; }
        public string BidderLastName { get; set; }
        public string Status { get; set;}
    }
}
