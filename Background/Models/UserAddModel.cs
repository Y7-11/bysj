using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class UserAddModel
    {
        public string nickname { get; set; }

        public string phonenum { get; set; }

        public string gender { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string account { get; set; }

        public string remark { get; set; }

        public long[] RoleIds { get; set; }
    }
}