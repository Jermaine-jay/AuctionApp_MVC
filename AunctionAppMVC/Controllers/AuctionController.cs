using AunctionApp.BLL.Interfaces;
using AunctionApp.BLL.Models;
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

        public async Task<IActionResult> GetAuction(int productId)
        {
            var model = await _ProductService.GetAuction(productId);
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> MakeBid(int productId)
        {
            var bidder = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return View(new AddOrUpdateBidVM { ProductId = productId, Bidder = bidder });
        }

        [Authorize]
        public async Task<IActionResult> Home()
        {
            var model = await _ProductService.GetAuctionsWithBidsAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize]
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
                return View("Home");
            }
            return View("Home");
        }
    }
}
