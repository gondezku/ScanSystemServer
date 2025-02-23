using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Hubs;
using static System.Net.Mime.MediaTypeNames;
using Models;
using Microsoft.AspNetCore.Identity;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;

namespace SacnSystemServer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDBContext db, UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _db = db;
            _usermanager = usermanager;
            _signInManager = signInManager;
            _logger = logger;
            //int siteku = _db.Sites.Count();

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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
