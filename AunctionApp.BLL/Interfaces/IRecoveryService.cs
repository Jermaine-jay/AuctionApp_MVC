using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AunctionApp.BLL.Interfaces
{
    public interface IRecoveryService
    {
        Task<(bool successful, string msg)> ForgotPassword(IUrlHelper urlHelper, ForgotPasswordVM model);
        Task<(bool successful, string msg)> ResetPassword(ResetPasswordVM model);
        Task<(bool successful, string msg)> ChangeEmail(string userId, string code);
        Task<(bool successful, string msg)> ResetEmail(string userId, string code);
    }
}
