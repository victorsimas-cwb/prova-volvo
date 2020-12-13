using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using VolvoTrucks.Services;
using VolvoTrucks.WebApp.Models;

namespace VolvoTrucks.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITruckService _service;

        public HomeController(ILogger<HomeController> logger, ITruckService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var res = _service.ListModels();
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
