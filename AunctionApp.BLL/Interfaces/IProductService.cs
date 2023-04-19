using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;

namespace AunctionApp.BLL.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<AunctionVMForm>> GetAunctions();
        public Task<IEnumerable<AunctionWithBidVM>> GetAunctionsWithBidsAsync();
        public Task<AunctionVMForm> GetAunction(int productId);

    }
}
