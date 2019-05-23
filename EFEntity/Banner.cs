namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner:Base
    {

        [Required]
        [StringLength(100)]
        public string bannerurl { get; set; }

        [Required]
        [StringLength(100)]
        public string bannertitle { get; set; }

        [Required]
        public string bannercontext { get; set; }

        [Required]
        [StringLength(100)]
        public string bannerlink { get; set; }

        public int bannerGrobal { get; set; }

        public bool display { get; set; }
    }
}
