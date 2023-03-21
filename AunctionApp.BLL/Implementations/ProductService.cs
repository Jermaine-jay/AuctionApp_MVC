using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Implementations
{
    public class ProductService : IProductService
    {
        public Task<(bool successful, string msg)> AddOrUpdateAsync(UpdateAunctionVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductWithBidVM>> GetProductsWithBidsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
