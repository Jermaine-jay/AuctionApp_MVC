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
        public UserController(IUserService userService, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }
        public IActionResult Home()
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
            if (user != null)
            {
                return View(profile);
            }
            return View(profile);
        }

        public IActionResult SignIn()
        {
            return View(new SignInVM());
        }

        [Authorize]
        public async Task<IActionResult> GetUser(string userId)
        {
            var model = await _userService.GetUser(userId);
            return View(model);
        }

        public IActionResult AddBid()
        {
            return View(new AddOrUpdateBidVM());
        }

        [Authorize]
        public async Task<IActionResult> UpdateUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return View(new RegisterVM());
            }
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
        [Authorize]
        public async Task<IActionResult> SaveUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.RegisterUser(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("SignIn");
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
                var (successful, msg) = await _userService.RegisterAdmin(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("SignIn");
                }
                TempData["ErrMsg"] = msg;
                return View("RegisterAdmin");
            }
            return View("RegisterAdmin");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.Update(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllUsers");
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(ProfileImageVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var (successful, msg) = await _userService.UpdateProfileImage(userId, model);

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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _userService.Delete(Id);
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
    }
}
