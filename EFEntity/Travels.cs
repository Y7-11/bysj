namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Travels:Base
    {

        public long Uid { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        public bool shenhe { get; set; }

        [Required]
        public string content { get; set; }

        public long? tops { get; set; }

    }
}
