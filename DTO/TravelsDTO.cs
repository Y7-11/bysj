using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TravelsDTO:BaseDTO
    {
        public string UNickName { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public bool shenhe { get; set; }

        public long? tops { get; set; }

        public long uid { get; set; }
    }
}
