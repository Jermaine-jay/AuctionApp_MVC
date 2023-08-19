using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AunctionApp.DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace AunctionApp.BLL.Implementations
{
    public class RecoveryService : IRecoveryService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceFactory _serviceFactory;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RecoveryService(UserManager<User> userManager, IUnitOfWork unitOfWork, IServiceFactory serviceFactory, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = _unitOfWork?.GetRepository<User>();

        }

        public Task<(bool successful, string msg)> ChangeEmail(string userId, string code)
        {
            throw new NotImplementedException();
        }


        public async Task<(bool successful, string msg)> ForgotPassword(ForgotPasswordVM model)
        {
            var verify = await _serviceFactory.GetService<IAuthenticationService>().VerifyEmail(model.Email);
            if (verify == false)
            {
                return (false, "Invalid Email Address");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return (false, "User doesn't exist");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext,
                action: "ResetPassword", controller: "User", values: new { UserId = user.Id, code });

            var page = _serviceFactory.GetService<IGenerateEmailVerificationPage>().PasswordResetPage(callbackUrl);

            await _serviceFactory.GetService<IAuthenticationService>().SendEmailAsync(model.Email, "Reset Password", page);
            return (true, "Reset Password Email Sent");
        }



        public Task<(bool successful, string msg)> ResetEmail(string userId, string code)
        {
            throw new NotImplementedException();
        }



        public async Task<(bool successful, string msg)> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (false, "User does not exist!");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return (false, $"Couldn't reset password, {error.Description}");
                }
            }
            return (true, "Password Reset Complete");
        }


        public async Task<(bool successful, string msg)> ChangeDetailToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return (false, "User doesn't exist");
            }

            var token = await _userManager.GenerateUserTokenAsync(user, "PasswordlessLoginTotpProvider", "passwordless-auth");
            var page = _serviceFactory.GetService<IGenerateEmailVerificationPage>().ChangePasswordPage(token);

            await _serviceFactory.GetService<IAuthenticationService>().SendEmailAsync(user.Email, "Change Details", page);
            return (true, "Change Detail Email Sent");
        }



        public async Task<(bool successful, string msg)> VerifyChangeDetailToken(ConfirmTokenVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (false, "User not forund.");
            }

            var isValid = await _userManager.VerifyUserTokenAsync(user, "PasswordlessLoginTotpProvider", "passwordless-auth", model.Token);
            if (isValid)
            {
                return (true, "Token verified");
            }
            return (false, "Verification failed");
        }



        public async Task<(bool successful, string msg)> ChangePassword(ChangePasswordVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return (false, "User does not exist!");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return (false, $"Couldn't reset password, {error.Description}");
                }
            }
            return (true, "Password Reset Complete");
        }
    }
}
