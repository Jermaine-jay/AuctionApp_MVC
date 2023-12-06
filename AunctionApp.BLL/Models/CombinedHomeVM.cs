using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class CombinedHomeVM
    {
        public AuctionVMForm AuctionVMForm { get; set; }
        public CommentVM Comment { get; set; }

    }
}
