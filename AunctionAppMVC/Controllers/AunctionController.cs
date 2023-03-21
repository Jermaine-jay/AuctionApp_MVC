using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    public class AunctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewAunction()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
