using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LineDTO : BaseDTO
    {
        public string intro { get; set; }

        public string title { get; set; }

        public long NumOfComment { get; set; }

        public long NumOfPeople { get; set; }

        public string Province { get; set; }

        public string city { get; set; }

        public double NowPrice { get; set; }

        public long PastPrice { get; set; }

        public double discount { get; set; }

        public DateTime Now { get; set; }
    }
}
