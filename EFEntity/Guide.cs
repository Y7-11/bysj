namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Guide")]
    public partial class Guide:Base
    {
        public long Users_Id { get; set; }

        public int level { get; set; }

        public bool isappointment { get; set; }

        public long jifen { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public string school { get; set; }

        public string schoolnum { get; set; }
        [StringLength(100)]
        public string picture { get; set; }

        public bool shenhe { get; set; }

        [Required]
        [StringLength(200)]
        public string intro { get; set; }

        //public User Users { get; set; }
        //public virtual User Users { get; set; }
    }
}
