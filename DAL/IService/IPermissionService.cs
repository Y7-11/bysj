using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IPermissionService:IServiceSupport
    {
        PermissionDTO[] GetAll();

        void MarkDeleted(long id);

        PermissionDTO GetById(long id);

        void AddPermIds(long roleId, long[] permIds);

        void UpdatePermIds(long roleId, long[] permIds);

        long AddPermission(string permName, string description);

        void UpdatePermission(long id, string permName, string description);

        PermissionDTO[] GetByRoleId(long roleId);
    }
}
