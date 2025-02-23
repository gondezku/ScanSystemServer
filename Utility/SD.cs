
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
}
