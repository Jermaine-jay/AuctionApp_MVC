using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.BLL.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Route("[controller]/[action]/{productid?}")]
    public class AdminController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly IAdminService _AdminService;
        private readonly IUserService _userService;

        public AdminController(IProductService productService, IAdminService adminService, IUserService userService)
        {
            _ProductService = productService;
            _AdminService = adminService;
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult NewAuction()
        {
            return View(new AuctionVM());
        }


       /* public async Task<IActionResult> AllUsers()
        {
            var model = await _userService.GetUsers();
            return View(model);
        }*/


        public async Task<IActionResult> UpdateUser(string userId)
        {
            var user = await _userService.GetUser(userId);
            return View(user);
        }

        public async Task<IActionResult> UpdateAuction(string productId)
        {
            var model = await _ProductService.GetAuction(productId);
            return View(model);

        }

        public async Task<IActionResult> AllAuctions(int pg=1)
        {
            var model = await _ProductService.GetAuctions();
            var count = model.Count();
            int pagesize = 10;
            if (pg < 1)
                pg = 1;

            var pager = new Pagination(count, pg, pagesize);
            var rescip = (pg - 1) * pagesize;
            var data = model.Skip(rescip).Take(pager.PageSize).ToList();

            this.ViewBag.AuctionsPagination = pager;
            return View(data);
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
        public async Task<IActionResult> Delete(Guid ProductId)
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

       
        public async Task<IActionResult> SaveStatus(Guid ProductId)
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
