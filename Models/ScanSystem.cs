using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Models
    {
        public class BUnit
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string Id { get; set; }
            [Required]
            public string Name { get; set; }
            public string Message { get; set; }
        }

        public class ProdModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string Id { get; set; }
            public string BUid { get; set; }
            public string ModelName { get; set; }
            public int PkgSize { get; set; }
            public double CycleTime { get; set; }
            public int HeadCon { get; set; }

        }

        public class TempScanItems
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string Id { get; set; }
            public string SerialNumber { get; set; }
            public DateTime ScanTime { get; set; } = DateTime.Now;
            public int PlanNumber {  get; set; }
            public int ActualNumber { get; set; }
            public string ModelId   { get; set; }
            public string LineProd {  get; set; }
            public string BUiD { get; set; }
        }

    }
