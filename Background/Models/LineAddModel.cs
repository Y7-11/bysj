using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class LineAddModel
    {
        public long id { get; set; }

        public string Province { get; set; }

        public string city { get; set; }

        public string intro { get; set; }

        public string title { get; set; }

        public long NowPrice { get; set; }

        public long PastPrice { get; set; }

        public float discount { get; set; }

        public DateTime CreatDateTime { get; set; }
    }
}