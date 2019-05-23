namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Line")]
    public partial class Line:Base
    {

        [Required]
        [StringLength(100)]
        public string intro { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        public string Province { get; set; }

        public string city { get; set; }

        public long NumOfComment { get; set; }

        public long NumOfPeople { get; set; }

        public double NowPrice { get; set; }

        public long PastPrice { get; set; }

        public double discount { get; set; }
    }
}
