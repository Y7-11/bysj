﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AdminUserDTO : BaseDTO
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
