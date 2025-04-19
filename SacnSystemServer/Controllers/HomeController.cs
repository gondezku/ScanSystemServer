using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Hubs;
using Models;
using Microsoft.AspNetCore.Identity;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using Utility;

namespace SacnSystemServer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHubContext<UserHub> _hub;

        public HomeController(ApplicationDBContext db, UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger, IHubContext<UserHub> hub)
        {
            _db = db;
            _usermanager = usermanager;
            _signInManager = signInManager;
            _logger = logger;
            _hub = hub;
            //int siteku = _db.Sites.Count();

        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ProdModel()
        {
            string UsrBU = _usermanager.GetUserAsync(this.User).Result.BU_id;
            try
            {
                ViewBag.objModel = _db.ProdModels.Where(x=>x.BUid == UsrBU).AsNoTracking().ToList();
                return PartialView();
            }
            catch
            {
                return Redirect("/");
            }
            
        }

        [HttpGet]
        public IActionResult CurrentProdn()
        {
            string UsrBU = _usermanager.GetUserAsync(this.User).Result.BU_id;
            var objBU = _db.BUnits.FirstOrDefaultAsync(x => x.Id == UsrBU).GetAwaiter().GetResult();
            try
            {
                if (objBU != null)
                {
                    //_hub.Clients.All.SendAsync("MonStat", ProdStat.prodStats.ToList());
                    return PartialView();
                }
                else
                {
                    //    _ = Logout();
                    return PartialView("Privacy");
                }

            }
            catch
            {
                return PartialView("Privacy");
            }

        }

        [HttpGet]
        public IActionResult AddModel()
        {
            string UsrBU = _usermanager.GetUserAsync(this.User).Result.BU_id;
            try
            {
                return PartialView();
            }
            catch
            {
                return Redirect("/");
            }

        }
        [HttpGet]
        public IActionResult importProdModel()
        {
            string UsrBU = _usermanager.GetUserAsync(this.User).Result.BU_id;
            try
            {
                return PartialView();
            }
            catch
            {
                return Redirect("/");
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadCsvModel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not selected");
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var csvData = new List<ProdModel>();
                while (!stream.EndOfStream)
                {
                    var line = await stream.ReadLineAsync();
                    var values = line.Split(',');

                    var record = new ProdModel
                    {
                        BUid = _usermanager.GetUserAsync(this.User).Result.BU_id,
                        ModelName = values[0],
                        PkgSize = int.Parse(values[1]),
                        CycleTime = double.Parse(values[2]),
                        HeadCon = int.Parse(values[3])
                    };

                    csvData.Add(record);
                }

                _db.ProdModels.AddRange(csvData);
                await _db.SaveChangesAsync();
            }

            return Ok("File uploaded and data saved successfully");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Updated today 10/7/2024
        public JsonResult SubmitAddModel()
        {
            var resp = new Dictionary<string, object>();
            resp.Add("status", "failed");
            resp.Add("msg", "");

            try
            {
                    string? ModelName = Request.Form["ModelName"];
                    int? PkgSize = Convert.ToInt32(Request.Form["PkgSize"]);
                    int? HeadCon = Convert.ToInt32(Request.Form["HC"]);
                    double? CT = Convert.ToDouble(Request.Form["CycleTime"]);

                    resp["status"] = "success";
                    return Json(resp);
                }
            catch
            {
                resp["msg"] = "Error";
                return Json(resp);
            }
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
