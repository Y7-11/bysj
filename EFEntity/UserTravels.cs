using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    [Table("UserTravels")]
    public class UserTravels:Base
    {

        public long UId { get; set; }


        public long TId { get; set; }
    }
}
