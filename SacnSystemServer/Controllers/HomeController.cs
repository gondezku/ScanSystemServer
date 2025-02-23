using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Hubs;


//using SacnSystemServer.Data;
using static System.Net.Mime.MediaTypeNames;
using Models;

namespace SacnSystemServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<UserHub> _userHub;
        private readonly ILogger<HomeController> _logger;
        //private ApplicationDBContext _db;

        public HomeController(ILogger<HomeController> logger, IHubContext<UserHub> userHub)
        {
            //_db = db;
            _logger = logger;
            _userHub = userHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomeProduction()
        {
            return PartialView("Privacy");
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
