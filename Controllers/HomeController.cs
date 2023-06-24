using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asp_MVC_letsTry.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDB_Content _context;

        public HomeController(ILogger<HomeController> logger, AppDB_Content context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}