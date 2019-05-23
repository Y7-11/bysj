using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntity
{
   public class AdminUserRoles:Base
    {

       public long AdminUserId { get; set; }

       public long RoleId { get; set; }
    }
}
