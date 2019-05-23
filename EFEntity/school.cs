using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    [Table("school")]
    public class school:Base
    {
        public string schoolname { get; set; }

  
    }
}
