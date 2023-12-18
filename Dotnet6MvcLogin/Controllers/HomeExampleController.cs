using Microsoft.AspNetCore.Mvc;

namespace Dotnet6MvcLogin.Controllers
{
    public class HomeExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
