using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GuideDTO:BaseDTO
    {

        public long Users_Id { get; set; }

        public int level { get; set; }

        public bool isappointment { get; set; }

        public string nickname { get; set; }
        public long jifen { get; set; }

        public string school { get; set; }
        public string schoolnum { get; set; }

        public bool shenhe { get; set; }
        public string picture { get; set; }

        public string intro { get; set; }
    }
}
