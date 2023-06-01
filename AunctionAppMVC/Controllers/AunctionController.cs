using AunctionApp.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    public class AunctionController : Controller
    {
        private readonly IProductService _ProductService;

        public AunctionController( IProductService productService)
        {       
            _ProductService = productService;      
        }

        public async Task<IActionResult> GetAunction(int productId)
        {
            /*var model = await _ProductService.GetAunction(productId);
            return View(model);*/
            return View();
        }
        public async Task<IActionResult> AllAunctions()
        {
            var model = await _ProductService.GetAunctions();
            return View(model);
        }
        public async Task<IActionResult> Home()
        {
           /* var model = await _ProductService.GetAunctionsWithBidsAsync();
            return View(model);*/
           return View();
        }
    }
}
