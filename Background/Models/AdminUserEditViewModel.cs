using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Background.Models
{
    public class AdminUserEditViewModel
    {

        public AdminUserDTO AdminUser { get; set; }
        public RoleDTO[] Roles { get; set; }

        /// <summary>
        /// 当前用户拥有的角色id
        /// </summary>
        public long[] UserRoleIds { get; set; }
    }
}