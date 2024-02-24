using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
