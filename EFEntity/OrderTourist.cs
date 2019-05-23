namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderTourist")]
    public partial class OrderTourist
    {
        public int id { get; set; }

        public int Oid { get; set; }

        public int Uid { get; set; }
    }
}
