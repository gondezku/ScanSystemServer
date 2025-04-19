using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Xml.Linq;
using Utility;

namespace SacnSystemServer.Hubs
{
    public class UserHub : Hub
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ApplicationDBContext _db;

        public UserHub(UserManager<ApplicationUser> usermanager, ApplicationDBContext db)
        {
            _usermanager = usermanager;
            _db = db;
            //int siteku = _db.Sites.Count();

        }
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            if (Context.User.Identity.Name == null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId,"Controll");
            }
            else
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Monitor");
                PopulateStat("Monitor");
            }
            //else
            //{
            //    string userName = _usermanager.GetUserAsync(Context.User).Result.BU_id;
            //    string BUName = _db.BUnits.FirstOrDefaultAsync(u => u.Id == userName).GetAwaiter().GetResult().Name;
            //    if (BUName != null)
            //    {
            //        Console.WriteLine("User: " + BUName);
            //    }
            //}
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            if (Context.User.Identity.Name == null)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, "Controll");
                var objStat = ProdStat.prodStats.FirstOrDefault(x=>x.ConnectionId==Context.ConnectionId);
                if (objStat != null)
                {
                    ProdStat.prodStats.Remove(objStat);
                }
                PopulateStat("Monitor");
            }
            else
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, "Monitor");

            }
            return base.OnDisconnectedAsync(exception);
        }
        public async Task PopulateStat(string Groups) 
        { 
            await Clients.Group("Monitor").SendAsync("MonStat", ProdStat.prodStats.ToList());
        }
        public async Task ProdnStats(string BUName, string LineName, string Model, int Act, int plan)
        {
            if (ProdStat.prodStats == null)
            {
                ProdStat.prodStats = new List<ProdStat>();
                ProdStat.prodStats.Add(
                    new ProdStat
                    {
                        BUName = BUName,
                        LineName = LineName,
                        Model = Model,
                        Plan = plan,
                        Act = Act,
                        ConnectionId = Context.ConnectionId
                    }
                ); ;
            }
            else
            {
                var objStat = ProdStat.prodStats.FirstOrDefault(x => x.BUName == BUName && x.LineName == LineName);
                if (objStat==null)
                {
                    ProdStat.prodStats = new List<ProdStat>();
                    ProdStat.prodStats.Add(
                        new ProdStat
                        {
                            BUName = BUName,
                            LineName = LineName,
                            Model = Model,
                            Plan = plan,
                            Act = Act,
                            ConnectionId = Context.ConnectionId
                        }
                    );
                }
                else 
                {
                    objStat.Model = Model;
                    objStat.Plan = plan;
                    objStat.Act = Act;
                }
            }

            await Clients.Group("Monitor").SendAsync("updateStat", ProdStat.prodStats.FirstOrDefault(x => x.BUName == BUName && x.LineName == LineName));
            await PopulateStat("Monitor");
            //return SD.MonControll.FirstOrDefault(x => x.LineName == Test);
        }
        public async Task GetMonCtrl(string Test)
        {
            //if (Test != "Monitoring")
            //{
                if (SD.MonControll == null)
                {
                    SD.MonControll = new List<SD>();
                    SD.MonControll.Add(
                        new SD
                        {
                            LineName = Test,
                        }
                    );
                }
                else
                {
                    if (SD.MonControll.FirstOrDefault(x => x.LineName == Test) == null)
                    {
                        SD.MonControll = new List<SD>();
                        SD.MonControll.Add(
                            new SD
                            {
                                LineName = Test,
                            }
                        );
                    }
                }
                await Clients.Group("Controll").SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == Test));
            //}
            //else
            //{
            //    //await Groups.AddToGroupAsync(Context.ConnectionId, "Monitoring");
            //    Console.WriteLine("ConnectionId: "+ Context.ConnectionId);
            //}

            //return SD.MonControll.FirstOrDefault(x => x.LineName == Test);
        }

        public async Task MonCtrl(string lineName, bool isTriggered, bool isCounted, int counted, string Model, string cmd)
        {
            var obj = SD.MonControll.FirstOrDefault(x => x.LineName == lineName);
            if (obj != null) 
            {
                switch (cmd)
                {
                    case "Trigger":
                        obj.isTriggered = true;
                        obj.Count = counted;
                        break;
                    case "counted":
                        obj.isCounted=true;
                        break;
                    case "modelChange":
                        obj.Model = Model;
                        break;
                    default:
                        obj.Model = Model;
                        obj.isTriggered = isTriggered;
                        obj.isCounted = isCounted;
                        obj.Count = counted;
                        break;
                }
            }
            
            await Clients.Group("Controll").SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == lineName));
        }
    }
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
}
