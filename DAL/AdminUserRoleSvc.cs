using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IService;
using EFEntity;

namespace DAL
{
    public class AdminUserRoleSvc:IAdminUserRoleService
    {
        public long[] GetRoleId(long adminuserid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<AdminUserRoles>bs=new BaseService<AdminUserRoles>(ctx);
                return bs.GetAll().Where(e => e.AdminUserId == adminuserid).Select(e => e.RoleId).ToArray();
            }
        }
    }
}
