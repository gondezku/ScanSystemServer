using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Hubs;


//using SacnSystemServer.Data;
using SacnSystemServer.Models;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet]

        public async Task<IActionResult> armingCount(string objLine)
        {
            try
            {

                string LineObj = objLine;
                var objName = SD.MonControll.Where(x=>x.LineName==objLine).ToList();
                if (objName != null)
                {
                    objName[0].isTriggered = true;
                }

                await _userHub.Clients.All.SendAsync("updateControll", objName[0]);

                return Accepted();
            }
            catch {
                return Accepted();
            }
        }

        [HttpGet]

        public async Task<IActionResult> ModelSet(string objLine, string model)
        {
            try
            {

                var objName = SD.MonControll.Where(x => x.LineName == objLine).ToList();
                if (objName != null)
                {
                    objName[0].Model = model;
                }

                await _userHub.Clients.All.SendAsync("updateControll", objName[0]);

                return Accepted();
            }
            catch
            {
                return Accepted();
            }
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
