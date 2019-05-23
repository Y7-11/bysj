using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IRoleService: IServiceSupport
    {
         long AddNew(string roleName);

         RoleDTO[] GetAll();

        void UpdateRoleIds(long adminUserId, long[] roleIds);

        long[] GetByAdminUserId(long adminUserId);

        RoleDTO[] GetByUserId(long UserId);

        void Update(long roleId, string roleName);

        RoleDTO[] GetByRoleId(long[] roleids);
        RoleDTO GetById(long Id);

        void MarkDeleted(long roleId);

        void AddRoleIds(long adminUserId, long[] roleIds);

    }
}
