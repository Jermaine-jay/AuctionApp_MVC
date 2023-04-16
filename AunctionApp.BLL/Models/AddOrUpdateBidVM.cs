using AunctionApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Models
{
    public class AddOrUpdateBidVM
    {
        public int BidId { get; set; }
        public string BidPrice { get; set; }
        public string BidderUsername { get; set; }
        public DateTime BidTime { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
