using Microsoft.AspNetCore.Mvc;

namespace HalloDoc_MVC_Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
