using AunctionApp.BLL.Models;

namespace AunctionApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<(bool successful, string msg)> Create(UserVM model);
        Task<(bool successful, string msg)> Update(UserVM model);
        Task<(bool successful, string msg)> Delete(int userId);
        Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model);
        Task<UserVM> GetUser(int userId);
        Task<IEnumerable<UserVM>> GetUsers();

        /*Task<(bool successful, string msg)> AddAsync(AddBidVM model);
        Task<(bool successful, string msg)> UpdateAsync(UpdateBidVM model);*/

    }
}
