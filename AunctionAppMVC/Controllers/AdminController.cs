using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]/{productid?}")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _ProductService;
        private readonly IAdminService _AdminService;

        public AdminController(IUserService userService, IProductService productService, IAdminService adminService)
        {
            _userService = userService;
            _ProductService = productService;
            _AdminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewAuction()
        {
            return View(new AuctionVM());
        }

        public async Task<IActionResult> UpdateAuction(int productId)
        {
            var model = await _ProductService.GetAuction(productId);
            return View(model);

        }

        public async Task<IActionResult> AllAuctions()
        {
            var model = await _ProductService.GetAuctions();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(AuctionVM model)
        {

            if (ModelState.IsValid)
            {
                var (successful, msg) = await _AdminService.CreateAuctionAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Home", "Auction");
                }

                TempData["ErrMsg"] = msg;

                return View("NewAuction");
            }
            return View("NewAuction");
        }

        [HttpPut]
        public async Task<IActionResult> Update(AuctionVM model)
        {

            if (ModelState.IsValid)
            {
                var (successful, msg) = await _AdminService.UpdateAuctionAsync(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAuctions");
                }

                TempData["ErrMsg"] = msg;
                return View("UpdateAuction");

            }
            return View("UpdateAuction");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int ProductId)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _AdminService.DeleteAuctionAsync(ProductId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAuctions");
                }

                TempData["ErrMsg"] = msg;
                return View("AllAuctions");

            }
            return View("AllAuctions");

        }

        public async Task<IActionResult> SaveStatus(int ProductId)
        {
            if (ModelState.IsValid)
            {
                var (success, msg) = await _AdminService.ToggleProductStatus(ProductId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("AllAuctions");
                }

                TempData["ErrMsg"] = msg;
                return View("AllAuctions");

            }
            return View("AllAuctions");
        }      

    }
}
