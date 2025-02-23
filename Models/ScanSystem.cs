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

    }
