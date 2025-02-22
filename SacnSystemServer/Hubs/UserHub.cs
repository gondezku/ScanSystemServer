using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SacnSystemServer.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalUsers { get; set; } = 0;
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
        }

        public async Task MonCtrl(string lineName, bool isTriggered, bool isCounted, int counted, string Model)
        {
            var obj = SD.MonControll.FirstOrDefault(x => x.LineName == lineName);
            if (obj != null) 
            { 
                obj.Model = Model;
                obj.isTriggered = isTriggered;
                obj.isCounted = isCounted;
                obj.Count = counted;
            }


            await Clients.All.SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == lineName));
        }
    }
}
