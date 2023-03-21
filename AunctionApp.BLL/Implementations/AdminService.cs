using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Implementations
{
    public class AdminService : IAdminService
    {
        public Task<(bool successful, string msg)> CreateAunctionAsync(CreateAunctionVM model)
        {
            throw new NotImplementedException();
        }

        public Task<(bool successful, string msg)> DeleteAunctionAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Done, string msg)> ToggleProductStatus(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool successful, string msg)> UpdateAunctionAsync(UpdateAunctionVM model)
        {
            throw new NotImplementedException();
        }
    }
}
