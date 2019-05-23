using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class TravelEditModel
    {
        public long id { get; set; }
        public string title { get; set; }

        public string context { get; set; }

        public long tops { get; set; }
        public long Uid { get; set; }
    }
}