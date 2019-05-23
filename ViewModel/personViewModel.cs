using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class personViewModel
    {
        public string account { get; set; }

        public string phonenum { get; set; }

        public string nickname { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string school { get; set; }
        public schoolDTO[] allschool { get; set; }

        public string intro { get; set; }

        public bool isyuyue { get; set; }
    }
}
