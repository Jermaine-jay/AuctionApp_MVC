using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AunctionApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<(bool successful, string msg)> Create(CreateUserVM model);
        Task<IEnumerable<User>> GetUsers();
        Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model);

        /*Task<(bool successful, string msg)> AddAsync(AddBidVM model);
        Task<(bool successful, string msg)> UpdateAsync(UpdateBidVM model);*/

    }
}
