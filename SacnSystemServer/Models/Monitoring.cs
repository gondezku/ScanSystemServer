using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SacnSystemServer.Models
{
    public class Monitoring
    {
        public string LineName { get; set; }
        public bool isTriggered { get; set; } = false;
        public bool isCounted { get; set; } = false;
        public string Model {  get; set; } = string.Empty;
        public int Counted { get; set; } = 0;
    }
}
