using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IService;
using EFEntity;

namespace DAL
{
   public class UserRoleService:IUserRoleService
    {
        public bool AddUserRole(long UserId, long[] roleids)
        {
            try
            {
                using (Dbcontext ctx = new Dbcontext())
                {
                    foreach (var roleid in roleids)
                    {
                        UserRole ur = new UserRole();
                        ur.UserId = UserId;
                        ur.roleid = roleid;
                        ctx.UserRole.Add(ur);
                    }

                    ctx.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
          
        }

        public long[] GetById(long userId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<UserRole> bs = new BaseService<UserRole>(ctx);
                long[] ids = bs.GetAll().Where(p => p.UserId == userId).Select(s => s.roleid).ToArray();
                return ids;
            }
        }
    }
}
