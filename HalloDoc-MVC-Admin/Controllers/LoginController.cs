using Microsoft.AspNetCore.Mvc;

namespace HalloDoc_MVC_Admin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
