using Microsoft.AspNetCore.SignalR;
using Utility;

namespace SacnSystemServer.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalUsers { get; set; } = 0;
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            Console.WriteLine(UserHandler.ConnectedIds.Count);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
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
                    }
                );
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

            await Clients.All.SendAsync("updateStat", ProdStat.prodStats.FirstOrDefault(x => x.BUName == BUName && x.LineName == LineName));
            //return SD.MonControll.FirstOrDefault(x => x.LineName == Test);
        }
        public async Task GetMonCtrl(string Test)
        {
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
                if (SD.MonControll.FirstOrDefault(x => x.LineName == Test)==null)
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

            await Clients.All.SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == Test));
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
            
            await Clients.All.SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == lineName));
        }
    }
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
}
