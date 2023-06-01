using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;          
        }
        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> AllUsers()
        {
            var model = await _userService.GetUsers();
            return View(model);
        }
        public async Task<IActionResult> GetUser(int userId)
        {
            var model = await _userService.GetUser(userId);
            return View(model);
        }

        public IActionResult AddBid()
        {
            return View(new AddOrUpdateBidVM());
        }

        public IActionResult CreateUser()
        {
            return View(new UserVM());
        }

        public IActionResult UpdateUser(int userId)
        {
            var model = _userService.GetUser(userId);
            return View(model);
        }

        public IActionResult DeleteUser(int userId)
        {
            return View(new UserVM { Id = userId });

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
        public async Task<IActionResult> Save(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _userService.Create(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllUsers");
                }

                TempData["ErrMsg"] = msg;

                return View("CreateUser");
            }

            return View("CreateUser");

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

        [HttpGet]
        public async Task<IActionResult> Delete(int userId)
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
    }
}
