using AunctionApp.BLL.Implementations;
using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View(new RegisterVM());
        }

        public IActionResult RegisterAdmin()
        {
            return View(new RegisterVM());
        }

        public async Task<IActionResult> Profile()
        {
            var profile = new ProfileVM();
            profile.User = new UserVM();
            profile.Image = new ProfileImageVM();
            /*var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.UserProfileAsync(userId);
            if (user == null)
            {
                return View(new UserVM());
            }*/
            return View(new ProfileVM());
        }

        public IActionResult SignIn()
        {
            return View(new SignInVM());
        }

        public async Task<IActionResult> AllUsers()
        {
            var model = await _userService.GetUsers();
            return View(model);
        }

        public async Task<IActionResult> GetUser(string userId)
        {
            var model = await _userService.GetUser(userId);
            return View(model);
        }

        public IActionResult AddBid()
        {
            return View(new AddOrUpdateBidVM());
        }

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

        /*public IActionResult DeleteUser(int userId)
        {
            return View(new UserVM { Id = userId });

        }*/

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
                return View("Profile");
            }
            return View("Profile");
        }

        public async Task<IActionResult> SignOut()
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.SignOut();
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Index", "Home");
                }
                TempData["ErrMsg"] = msg;
                return View("Index", "Home");
            }
            return View("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _userService.Delete(userId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllUsers");
                }

                TempData["ErrMsg"] = msg;
                return RedirectToAction("AllUsers");

            }
            return View("AllUsers");

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfileImage(ProfileImageVM model)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.UpdateProfileImage(userId,model);

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
    }
}
