using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO : BaseDTO
    {
        public string PhoneNum { get; set; }
        public string NickName { get; set; }
        public string Account { get; set; }
        public string Gender { get; set; }
        public string Remark { get; set; }
        public string Password { get; set; }
        public long Level { get; set; }
        public string Email { get; set; }
    }
}
