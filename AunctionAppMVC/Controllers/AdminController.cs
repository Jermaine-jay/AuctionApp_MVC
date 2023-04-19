using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _ProductService;
        private readonly IAdminService _AdminService;

        public AdminController(IUserService userService, IProductService _todoService, IAdminService adminService)
        {
            _userService = userService;
            _ProductService = _todoService;
            _AdminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewAunction()
        {
            return View(new AunctionVM());
        }
        public async Task<IActionResult> UpdateAunction(int ProductId)
        {
            var user = await _ProductService.GetAunction(ProductId);
            return View(user);
        }
        public IActionResult DeleteAunction(int ProductId)
        {
            return View(new AunctionVMForm { ProductId = ProductId });
        }
        public async Task<IActionResult> UpdateStatus(int productId)
        {
            return View(new AunctionVMForm { ProductId = productId });
        }

        [HttpPost]
        public async Task<IActionResult> Save(AunctionVM model)
        {

            if (ModelState.IsValid)
            {
                var (successful, msg) = await _AdminService.CreateAunctionAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("AllAunctionsWithBids");
                }

                TempData["ErrMsg"] = msg;

                return View("NewAunction");
            }

            return View("NewAunction");

        }

        [HttpPut]
        public async Task<IActionResult> Update(AunctionVM model)
        {

            if (ModelState.IsValid)
            {
                var (successful, msg) = await _AdminService.UpdateAunctionAsync(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAunctions");
                }

                TempData["ErrMsg"] = msg;
                return View("UpdateAunction");

            }
            return View("UpdateAunction");
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            if (ModelState.IsValid)
            {

                var (success, msg) = await _AdminService.DeleteAunctionAsync(ProductId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAunctions");
                }

                TempData["ErrMsg"] = msg;
                return RedirectToAction("AllAunctions");

            }
            return View("AllAunctions");

        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> SaveStatus(int ProductId)
        {
            if (ModelState.IsValid)
            {

                var (success, msg) = await _AdminService.ToggleProductStatus(ProductId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAunctions");
                }

                TempData["ErrMsg"] = msg;
                return RedirectToAction("AllAunctions");

            }
            return View("AllAunctions");

        }


    }
}
