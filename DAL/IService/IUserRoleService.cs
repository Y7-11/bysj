using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IUserRoleService:IServiceSupport
    {
        bool AddUserRole(long UserId, long[] roleids);

        long[] GetById(long userId);
    }
}
