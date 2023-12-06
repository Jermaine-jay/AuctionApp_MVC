using AunctionApp.BLL.Models;

namespace AunctionApp.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<(bool successful, string msg)> CreateAuctionAsync(AuctionVM model);
        Task<(bool successful, string msg)> UpdateAuctionAsync(AuctionVM model);
        Task<(bool successful, string msg)> DeleteAuctionAsync(Guid productId);
        Task<(bool Done, string msg)> ToggleProductStatus(Guid productId);

    }
}
