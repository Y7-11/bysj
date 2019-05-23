using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class RoleEditModel
    {   
        public long Id { get; set; }
        public string Name { get; set; }
        public long[] PermissionIds { get; set; }
    }
}