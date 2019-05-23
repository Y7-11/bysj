using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommentDTO:BaseDTO
    {
        public string Context { get; set; }

        public long Uid { get; set; }

        public string UserName { get; set; }

        public string title { get; set; }

        public string Email { get; set; }

        public string htmlcode { get; set; }

        public string PhoneNum { get; set; }
    }
}
