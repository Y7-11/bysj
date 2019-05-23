using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class AddTravelsModel
    {

        public long id { get; set; }

        public DateTime CreatDateTime { get; set; }

        public string title { get; set; }

        public string content { get; set; }

        public long? tops { get; set; }

        public long uid { get; set; }
    }
}