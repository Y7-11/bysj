namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment:Base
    {
        public string Context { get; set; }
        [ForeignKey("Raiders")]
        public long Typeid { get; set; }

        [ForeignKey("User")]
        public long Uid { get; set; }


        public  User User { get; set; } 

      
        public  Raiders Raiders { get; set; } 
    }
}
