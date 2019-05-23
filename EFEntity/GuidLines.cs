using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    [Table("GuidLines")]
    public partial class GuidLines : Base
    {
        public long Gid { get; set; }

        public long Lid { get; set; }

    }
}
