using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
    [Table("LinesInfo")]
    public class LinesInfo:Base
    {
        public long Lid { get; set; }

        public string Address { get; set; }

        public string ScenicSpot { get; set; }

        public string SpotInfo { get; set; }

        public string Apartment { get; set; }

        public string attention { get; set; }

        public string Level { get; set; }

        public string Facilities { get; set; }

        public long tag { get; set; }
    }
}
