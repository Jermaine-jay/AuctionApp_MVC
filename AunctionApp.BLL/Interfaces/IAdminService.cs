using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<(bool successful, string msg)> CreateAuctionAsync(AuctionVM model);
        Task<(bool successful, string msg)> UpdateAuctionAsync(AuctionVM model);
        Task<(bool successful, string msg)> DeleteAuctionAsync(int productId);
        Task<(bool Done, string msg)> ToggleProductStatus(int productId);

    }
}
