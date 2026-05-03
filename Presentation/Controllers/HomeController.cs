using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize (Policy = "Authentication Management")]
        public IActionResult Index()
        {
            var isAuth = User.Identity?.IsAuthenticated;
            var authType = User.Identity?.AuthenticationType;
            var claims = User.Claims.ToList();
            _logger.LogInformation("User is logged to the Index of home controler");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
