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
    public class UserService : IUserService
    {
        public Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model)
        {
            throw new NotImplementedException();
        }

        public Task<(bool successful, string msg)> Create(CreateUserVM model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
