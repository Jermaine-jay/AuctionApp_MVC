using AunctionApp.BLL.Implementations;
using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IRecoveryService _recoveryService;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService, IProductService productService, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IRecoveryService recoveryService, IAuthenticationService authenticationService )
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _urlHelperFactory = urlHelperFactory;
            _recoveryService = recoveryService;
            _authenticationService = authenticationService;
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult WaitingPage()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var model = await _userService.GetUsers();
            return View(model);
        }

        public IActionResult RegisterUser()
        {
            return View(new RegisterVM());
        }

        [Authorize(Roles = "Admin")]
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


        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var (successful, msg) = await _authenticationService.ConfirmEmail(userId, code);
            if (successful)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("SignIn");
            }
            TempData["ErrMsg"] = msg;
            return View("SignIn");
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
                var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                var (successful, msg) = await _userService.RegisterUser(urlHelper, model);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveAdmin(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                var (successful, msg) = await _userService.RegisterAdmin(urlHelper,model);
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
                var (successful, msg) = await _userService.Update(model);

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

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.SignIn(model);
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
                var (successful, msg) = await _userService.SignOut();
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
                var (successful, msg) = await _userService.UpdateProfileImage(model.Image, userId);
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


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _userService.Delete(userId);
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
                var (successful, msg) = await _recoveryService.ResetPassword(model);
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
                var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                var (successful, msg) = await _recoveryService.ForgotPassword(urlHelper, model);

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

    }
}
