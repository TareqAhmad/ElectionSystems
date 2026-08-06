
using Microsoft.AspNetCore.Mvc;

namespace Ejmmaa.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}