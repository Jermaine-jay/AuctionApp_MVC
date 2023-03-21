using AunctionApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<(bool successful, string msg)> UpdateAunctionAsync(UpdateAunctionVM model);
        Task<(bool successful, string msg)> CreateAunctionAsync(CreateAunctionVM model);

        Task<(bool successful, string msg)> DeleteAunctionAsync(int productId);
        Task<(bool Done, string msg)> ToggleProductStatus(int productId);
    }
}
