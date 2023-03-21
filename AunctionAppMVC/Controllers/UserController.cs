using Microsoft.AspNetCore.Mvc;

namespace AunctionAppMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
