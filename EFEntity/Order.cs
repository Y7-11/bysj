namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order:Base
    {

        public string OrderNum { get; set; }

        public long tid { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public double Money { get; set; }

        public int state { get; set; }

    }
}
