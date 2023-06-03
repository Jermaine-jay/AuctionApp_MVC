using AunctionApp.BLL.Interfaces;
using AunctionAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AunctionAppMVC.Controllers
{
    //[Route("[controller]/[action]/{id?}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _ProductService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _ProductService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _ProductService.GetAuctions();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}