using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceFactory _serviceFactory;

        public UserController(IUserService userService, IProductService productService, IServiceFactory serviceFactory, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _serviceFactory = serviceFactory;

        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult WaitingPage()
        {
            return View();
        }

        [Authorize(Roles ="Admin, SuperAdmin")]
        public async Task<IActionResult> AllUsers()
        {
            var model = await _userService.GetUsers();
            return View(model);
        }


        public IActionResult RegisterUser()
        {
            return View(new RegisterVM());
        }



        [Authorize(Roles = "SuperAdmin")]
        public IActionResult RegisterAdmin()
        {
            return View(new RegisterVM());
        }



        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.UserProfileAsync(userId);

            var profile = new ProfileVM()
            {
                User = user,
                Image = new ProfileImageVM(),
            };

            return View(profile);

        }

        public IActionResult SignIn()
        {
            return View(new SignInVM());
        }


        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordVM());
        }



        public IActionResult ResetPassword(string? code, string userId)
        {
            if (code == null)
            {
                return RedirectToAction("Error");
            }
            var model = new ResetPasswordVM { Code = code, UserId = userId };
            return View(model);
        }



        [Authorize]
        public async Task<IActionResult> GetUser(string userId)
        {
            var model = await _userService.GetUser(userId);
            return View(model);
        }



        [Authorize]
        public IActionResult AddBid()
        {
            return View(new AddOrUpdateBidVM());
        }



        [Authorize]
        public async Task<IActionResult> UpdateUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUser(userId);
            return View(user);
        }



        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserBids()
        {
            var bidder = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var model = await _productService.GetUserBidsAsync(bidder);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveBid(AddOrUpdateBidVM model)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _userService.AddOrUpdateBidAsync(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Index");
                }

                TempData["ErrMsg"] = msg;
                return View("New");
            }
            return View("New");
        }



        [HttpPost]
        public async Task<IActionResult> SaveUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().RegisterUser(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("WaitingPage");
                }
                TempData["ErrMsg"] = msg;
                return View("RegisterUser");
            }
            return View("RegisterUser");
        }



        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> SaveAdmin(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().RegisterAdmin(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("WaitingPage");
                }
                TempData["ErrMsg"] = msg;
                return View("RegisterAdmin");
            }
            return View("RegisterAdmin");
        }



        [HttpPost]
        public async Task<IActionResult> Update(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().Update(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Profile");
                }

                TempData["ErrMsg"] = msg;
                return View("UpdateUser");

            }
            return View("UpdateUser");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var (successful, msg) = await _serviceFactory.GetService<IAuthenticationService>().ConfirmEmail(userId, code);
            if (successful)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("SignIn");
            }
            TempData["ErrMsg"] = msg;
            return View("SignIn");
        }



        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().SignIn(model);
                if (successful)
                {

                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Profile");
                }
                TempData["ErrMsg"] = msg;
                return View("SignIn");
            }
            return View("SignIn");
        }



        public async Task<IActionResult> SignOut()
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().SignOut();
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("SignIn");
                }
                TempData["ErrMsg"] = msg;
                return View("SignIn");
            }
            return View("SignIn");
        }



        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(ProfileVM model)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IUserService>().UpdateProfileImage(model.Image, userId);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Profile");
                }

                TempData["ErrMsg"] = msg;
                return View("Profile");
            }
            return View("Profile");
        }



        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _serviceFactory.GetService<IUserService>().Delete(userId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Users");
                }

                TempData["ErrMsg"] = msg;
                return View("Users");

            }
            return View("Users");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetUserPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IRecoveryService>().ResetPassword(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("SignIn");
                }
                TempData["ErrMsg"] = msg;
                return View("ResetPassword");
            }
            return View("ResetPassword");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IRecoveryService>().ForgotPassword(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("WaitingPage");
                }
                TempData["ErrMsg"] = msg;
                return View("ForgotPassword");
            }
            return View("ForgotPassword");

        }



        public async Task<ActionResult> ChangePassword()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new ChangePasswordVM { UserId = userId });
        }



        public async Task<IActionResult> ConfirmToken()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new ConfirmTokenVM { UserId = userId });
        }



        public async Task<IActionResult> UserChangePassword()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (successful, msg) = await _serviceFactory.GetService<IRecoveryService>().ChangeDetailToken(userId);
            if (successful)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("ConfirmToken");
            }
            TempData["ErrMsg"] = msg;
            return View("WaitingPage");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmUserToken(ConfirmTokenVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IRecoveryService>().VerifyChangeDetailToken(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("ChangePassword");
                }
                TempData["ErrMsg"] = msg;
                return View("ConfirmToken");
            }
            return View("ConfirmToken");

        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _serviceFactory.GetService<IRecoveryService>().ChangePassword(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Profile");
                }
                TempData["ErrMsg"] = msg;
                return View("Confirm");
            }
            return View("WaitingPage");

        }

    }
}
