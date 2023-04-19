using AunctionApp.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    public class AunctionController : Controller
    {
        private readonly IProductService _ProductService;

        public AunctionController( IProductService productService)
        {
           
            _ProductService = productService;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAunction(int productId)
        {
            var model = await _ProductService.GetAunction(productId);
            return View(model);
        }
        public async Task<IActionResult> AllAunctions()
        {
            var model = await _ProductService.GetAunctions();
            return View(model);
        }
        public async Task<IActionResult> AllAunctionsWithBids()
        {
            var model = await _ProductService.GetAunctionsWithBidsAsync();
            return View(model);
        }
    }
}
