using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class CommentEditModel
    {
        public long id { get; set; }
        public string email { get; set; }
        public string context { get; set; }
        public string phonenum { get; set; }
    }
}