using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;

namespace AunctionApp.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<AuctionVMForm>> GetAuctions();
        public Task<IEnumerable<AuctionWithBidVM>> GetAuctionsWithBidsAsync();
        public Task<AuctionVMForm> GetAuction(int Id);
        Task<(bool successful, string msg)> AddOrUpdateAsync(AddOrUpdateBidVM model);
        Task<IEnumerable<UserBidsVM>> GetUserBidsAsync(string bidder);
        Task<BidVM> GetHighestBidderAsync(int ProductId);

    }
}
