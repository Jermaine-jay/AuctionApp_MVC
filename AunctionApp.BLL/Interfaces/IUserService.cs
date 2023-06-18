using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;

namespace AunctionApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task<(bool successful, string msg)> Update(UserVM model);
        Task<(bool successful, string msg)> Delete(string userId);
        Task<(bool successful, string msg)> AddOrUpdateBidAsync(AddOrUpdateBidVM model);
        Task<(bool successful, string msg)> SignIn(SignInVM signIn);
        Task<(bool successful, string msg)> SignOut();
        Task<UserVM> GetUser(string userId);
        Task<IEnumerable<UserVM>> GetUsers();
        Task<(bool successful, string msg)> RegisterAdmin(RegisterVM register);
        Task<(bool successful, string msg)> RegisterUser(RegisterVM register);
        Task<UserVM> UserProfileAsync(string userId);
        Task<(bool successful, string msg)> UpdateProfileImage(ProfileImageVM model);

    }
}
