using Microsoft.AspNetCore.Mvc;

namespace Dotnet6MvcLogin.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
