using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class UserBidsVM
    {
        public string ProductName { get; set; }
        public string ActualAmount { get; set; }
        public string Status { get; set; }
        public string BidPrice { get; set; }
        public string BidTime { get; set; }
    }
}
