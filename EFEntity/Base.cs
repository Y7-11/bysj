using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    public abstract class Base
    {
        public Base()
        {
            this.CreateDateTime=DateTime.Now;
            this.IsDeleted=false;
        }
        [Key]
        public long Id { get; set; }

        public DateTime CreateDateTime { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
