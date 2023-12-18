using Dotnet6MvcLogin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Dotnet6MvcLogin.Repositories.Abstract;

namespace Dotnet6MvcLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var items = _itemService.List(term, true, currentPage);
            return View(items);
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Privacy(string term = "", int currentPage = 1)
        {
            
            var items = _itemService.List(term, true, currentPage);
            return View(items);
        }

        public IActionResult ItemDetail(int itemId)
        {
            var item = _itemService.GetById(itemId);
            return View(item);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}