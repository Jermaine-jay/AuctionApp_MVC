using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class AddOrUpdateBidVM
    {
        public int BidId { get; set; }
        public string BidderFirstName { get; set; }
        public string BidderLastName { get; set; }
        public string Description { get; set; }

        public DateTime BidTime { get; set; }
    }
}
