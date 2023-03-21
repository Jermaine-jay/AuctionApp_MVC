using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        Task<IEnumerable<ProductWithBidVM>> GetProductsWithBidsAsync();

        //Task<(bool successful, string msg)> AddOrUpdateAsync(UpdateAunctionVM model);

    }
}
