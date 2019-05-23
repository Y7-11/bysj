namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("T-Line")]
    public partial class T_Line:Base
    {

        public int Tid { get; set; }

        public int Lid { get; set; }
    }
}
