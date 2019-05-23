namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderUser")]
    public partial class OrderUser:Base
    {

        public long Oid { get; set; }

        public long Uid { get; set; }
    }
}
