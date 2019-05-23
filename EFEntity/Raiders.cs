namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Raiders:Base
    {

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        [StringLength(200)]
        public string content { get; set; }

          [ForeignKey("Typeid")]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
