using Microsoft.AspNetCore.SignalR;
using SacnSystemServer.Models;

namespace SacnSystemServer.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalUsers { get; set; } = 0;
        public async Task GetMonCtrl(string Test)
        {
            if (SD.MonControll == null)
            {
                SD.MonControll = new List<Monitoring>();
                SD.MonControll.Add(
                    new Monitoring
                    {
                        LineName = Test,
                    }
                );
            }
            else 
            {
                if (SD.MonControll.FirstOrDefault(x => x.LineName == Test) == null)
                {
                    SD.MonControll = new List<Monitoring>();
                    SD.MonControll.Add(
                        new Monitoring
                        {
                            LineName = Test,
                        }
                    );
                }
            }
            
            await Clients.All.SendAsync("updateControll", SD.MonControll.FirstOrDefault(x => x.LineName == Test));
        }

        


    }
}
