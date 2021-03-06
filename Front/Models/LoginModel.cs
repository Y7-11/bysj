﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(11, MinimumLength = 6)]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string VerifyCode { get; set; }
    }
}