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
    public  class RoleService:IRoleService
    {
        public long AddNew(string roleName)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                bool exists = bs.GetAll().Any(e => e.Name == roleName);
                if (exists)
                {
                    throw new ArgumentException("角色名字已经存在" + roleName);
                }
                Role role = new Role();
                role.Name = roleName;
                ctx.Role.Add(role);
                ctx.SaveChanges();
                return role.Id;
            }
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> userBs = new BaseService<AdminUser>(ctx);
                var user = userBs.GetById(adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + adminUserId);
                }
                user.Roles.Clear();
                BaseService<Role> roleBs = new BaseService<Role>(ctx);
                var roles = roleBs.GetAll().Where(p => roleIds.Contains(p.Id)).ToArray();
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }
                ctx.SaveChanges();
            }
        }

        public long[] GetByAdminUserId(long adminUserId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUserRoles> bs = new BaseService<AdminUserRoles>(ctx);
                var ids = bs.GetAll().Where(e=>e.AdminUserId==adminUserId).ToList().Select(e=>e.RoleId);
                return ids.ToArray();
            }
        }
        public RoleDTO[] GetByUserId(long UserId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<UserRole> bs = new BaseService<UserRole>(ctx);
                BaseService<Role> rolebs = new BaseService<Role>(ctx);
                var roleids = bs.GetAll().Where(e => e.UserId == UserId).ToList().Select(e =>e.roleid).ToArray();
                List<Role>list=new List<Role>();
                foreach (var roleid in roleids)
                {
                    var r = rolebs.GetById(roleid);
                    list.Add(r);
                }
                //if (user == null)
                //{
                //    throw new ArgumentException("用户不存在" + UserId);
                //}

                return list.Select(e => ToDTO(e)).ToArray();
            }
        }
        
        public void Update(long roleId, string roleName)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> roleBS = new BaseService<Role>(ctx);
                bool exists = roleBS.GetAll().Any(r => r.Name == roleName && r.Id != roleId);
                if (exists)
                {
                    throw new ArgumentException("角色名字已存在" + roleName);
                }
                Role role = new Role();
                role.Id = roleId;
                ctx.Entry(role).State = System.Data.Entity.EntityState.Unchanged;
                role.Name = roleName;
                ctx.SaveChanges();
            }
        }

        public RoleDTO GetById(long Id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                var role = bs.GetById(Id);
                return role == null ? null : ToDTO(role);
            }
        }

        public  RoleDTO[] GetAll()
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public RoleDTO[] GetByRoleId(long[] roleids)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                List<RoleDTO> list=new List<RoleDTO>();
                foreach (var roleid in roleids)
                {
                    var role= bs.GetAll().Where(e => e.Id == roleid).ToList().Select(e => ToDTO(e));
                    list.AddRange(role);
                }

                return list.ToArray();

            }
        }

        private  RoleDTO ToDTO(Role r)
        {
            RoleDTO dto = new RoleDTO();
            dto.CreateDateTime = r.CreateDateTime;
            dto.Id = r.Id;
            dto.Name = r.Name;
            return dto;
        }

        public  void MarkDeleted(long roleId)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Role> bs = new BaseService<Role>(ctx);
                bs.MarkDeleted(roleId);
            }
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> userBs = new BaseService<AdminUser>(ctx);
                var user = userBs.GetById(adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("用户不存在" + adminUserId);
                }
                var roleBs = ctx.Role.Where(e => e.IsDeleted == false);
                var roles = roleBs.Where(p => roleIds.Contains(p.Id)).ToArray();
                foreach (var role in roles)
                {
                    UserRole userrole = new UserRole();
                    userrole.roleid = (int)role.Id;
                    userrole.UserId = (int)user.Id;
                    ctx.UserRole.Add(userrole);
                    user.Roles.Add(role);
                }
                ctx.SaveChanges();
            }
        }

    }
}
