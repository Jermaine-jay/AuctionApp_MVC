using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
using AunctionApp.BLL.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{productId?}")]
    public class AuctionController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly IUserService _UserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuctionController(IProductService productService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _ProductService = productService;
            _UserService = userService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> GetAuction(string productId)
        {
            var model = await _ProductService.GetAuction(productId);
            return View(model);
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> MakeBid(string productId)
        {
            var bidder = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return View(new AddOrUpdateBidVM { ProductId = productId, Bidder = bidder });
        }


        [Authorize]
        public async Task<IActionResult> Home(int pg=1)
        {
            var model = await _ProductService.GetAuctionsWithBidsAsync();
            var count = model.Count();
            int pagesize = 3;
            if(pg < 1)
                pg= 1;

            var pager = new Pagination(count, pg, pagesize);
            var rescip = (pg - 1) * pagesize;
            var data= model.Skip(rescip).Take(pager.PageSize).ToList();   
            
            this.ViewBag.Pagination = pager;

            return View(data);
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SaveBid(AddOrUpdateBidVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _ProductService.AddOrUpdateAsync(model);
                if (successful)
                {

                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Home");
                }
                TempData["ErrMsg"] = msg;
                return View("MakeBid");
            }
            return View("Home");
        }
    }
}
