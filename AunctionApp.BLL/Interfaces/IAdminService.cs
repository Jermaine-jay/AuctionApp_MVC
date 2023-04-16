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
        Task<(bool successful, string msg)> CreateAunctionAsync(AunctionVM model);
        Task<(bool successful, string msg)> UpdateAunctionAsync(AunctionVM model);
        Task<(bool successful, string msg)> DeleteAunctionAsync(int productId);
        Task<(bool Done, string msg)> ToggleProductStatus(int productId);
        Task<IEnumerable<UserVM>> GetUsers();
        Task<IEnumerable<AunctionVMForm>> GetAunctions();

    }
}
