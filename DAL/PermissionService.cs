using DAL.IService;
using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class PermissionService: IPermissionService
    {
        public  PermissionDTO[] GetAll()
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Permission> bs = new BaseService<Permission>(ctx);
                return bs.GetAll().ToList().Select(c => ToDTO(c)).ToArray();
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                return bs.GetById(roleId).Permissions.Select(e => ToDTO(e)).ToArray();
            }
        }

        public void UpdatePermission(long id, string permName, string description)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Permission> bs = new BaseService<Permission>(ctx);
                var perm = bs.GetById(id);
                if (perm == null)
                {
                    throw new ArgumentException("id不存在" + id);
                }
                perm.Name = permName;
                perm.Description = description;
                ctx.SaveChanges();
            }
        }

        public long AddPermission(string permName, string description)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Permission> permBS = new BaseService<Permission>(ctx);
                bool exists = permBS.GetAll().Any(p => p.Name == permName);
                if (exists)
                {
                    throw new ArgumentException("权限项已经存在");
                }
                Permission perm = new Permission();
                perm.Name = permName;
                perm.Description = description;       
                ctx.Permission.Add(perm);
                ctx.SaveChanges();
                return perm.Id;
            }
        }


        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> roleBs = new BaseService<Role>(ctx);
                var role = roleBs.GetById(roleId);
                if (role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
                role.Permissions.Clear();
                BaseService<Permission> permBs = new BaseService<Permission>(ctx);
                var perms = permBs.GetAll().Where(p => permIds.Contains(p.Id)).ToArray();
                foreach (var perm in perms)
                {
                    role.Permissions.Add(perm);
                }
                ctx.SaveChanges();
            }

        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Permission> bs = new BaseService<Permission>(ctx);
                bs.MarkDeleted(id);
            }
        }

        private  PermissionDTO ToDTO(Permission p)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = p.CreateDateTime;
            dto.Description = p.Description;
            dto.Id = p.Id;
            dto.Name = p.Name;
            return dto;
        }

        public PermissionDTO GetById(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Permission> bs = new BaseService<Permission>(ctx);
                var pe = bs.GetById(id);
                return pe == null ? null : ToDTO(pe);
            }
        }

        public  void AddPermIds(long roleId, long[] permIds)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> roleBs = new BaseService<Role>(ctx);
                var role = roleBs.GetById(roleId);
                if (role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
         
                var permBs = ctx.Permission.Where(h => h.IsDeleted == false);
                var perms = permBs.Where(p => permIds.Contains(p.Id)).ToArray();
                foreach (var perm in perms)
                {
                    role.Permissions.Add(perm);
                }
                ctx.SaveChanges();
            }
        }
    }
}
