using SacnSystemServer.Models;

namespace SacnSystemServer
{
    public class SD
    {
        public string LineName { get; set; }
        public bool isTriggered { get; set; } = false;
        public bool isCounted { get; set; } = false;

        public static List<Monitoring> MonControll { get; set; }
    }
}
