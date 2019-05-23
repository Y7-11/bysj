using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    [Table("GuideUser")]
    public partial class GuideUser:Base
    {
        public long GuideId { get; set; }
        public long Uid { get; set; }
    }
}
