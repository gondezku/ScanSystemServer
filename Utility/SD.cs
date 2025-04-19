
namespace Utility
{
    public class SD
    {
        public string LineName { get; set; }
        public bool isTriggered { get; set; } = false;
        public bool isCounted { get; set; } = false;
        public string Model { get; set; } = "";
        public int Count { get; set; } = 0;

        public static List<SD> MonControll { get; set; }
    }

    public class ProdStat
    {
        public string BUName { get; set; }
        public string LineName { get; set; }
        public string Model { get; set; }
        public int Plan { get; set; }
        public int Act { get; set; }
        public string? ConnectionId { get; set; }
    
        public static List<ProdStat> prodStats { get; set; }
    }

    public class BroadCastMsg
    {
        public string BUName { get; set; }
        public string msgBroad { get; set; }
        public static List<BroadCastMsg> broadCastMsg { get; set; }
    }

        public static class Roles
    {
        public const string Role_Manager = "Manager";
        public const string Role_Production = "Production";
        public const string Role_PQC = "PQC";
    }
}
