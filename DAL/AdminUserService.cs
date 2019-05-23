using Common;
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
   public class AdminUserService: IAdminUserService
    {
        public  bool CheckLogin(string phoneNum, string password)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                var user = bs.GetAll().SingleOrDefault(e => e.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    string dbpwdHash = user.PasswordHash;
                    string salt = user.PasswordSalt;
                    string userPwdHash = commonhelper.CalcMD5(salt + password);
                    return dbpwdHash == userPwdHash;
                }
            }
        }

        public void UpdateAdminUser(long id, string name, string phoneNum,
string password, string email)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs
                    = new BaseService<AdminUser>(ctx);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的管理员");
                }
                user.Name = name;
                user.PhoneNum = phoneNum;
                user.Email = email;
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash =
                    commonhelper.CalcMD5(user.PasswordSalt + password);
                }
                ctx.SaveChanges();
            }
        }

        public AdminUserDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public  AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                var user = bs.GetAll().ToList().
                   Where(e => e.PhoneNum == phoneNum);
                var count = user.Count();
                if (count <= 0)
                {
                    return null;
                }
                else if (count == 1)
                {
                    return ToDTO(user.SingleOrDefault());
                }
                else
                {
                    throw new ArgumentException("找到多个手机号为" + phoneNum + "的管理");
                }

            }
        }

        public long AddAdminUser(string name, string phoneNum, string password, string email)
        {
            AdminUser user = new AdminUser();
            user.Email = email;
            user.Name = name;
            user.PhoneNum = phoneNum;
            var salt = commonhelper.CreateVerifyCode(4);
            user.PasswordSalt = salt;
            string pwdHash = commonhelper.CalcMD5(salt + password);
            user.PasswordHash = pwdHash;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                bool exists = bs.GetAll().Any(e => e.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已经存在" + phoneNum);
                }
                ctx.AdminUser.Add(user);
                ctx.SaveChanges();
            }
            return user.Id;
        }

        private  AdminUserDTO ToDTO(AdminUser u)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CreateDateTime = u.CreateDateTime;
            dto.Id = u.Id;
            dto.PhoneNum = u.PhoneNum;
            dto.Email = u.Email;
            dto.Name = u.Name;
            return dto;
        }

        public void MarkDeleted(long adminUserId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                bs.MarkDeleted(adminUserId);
            }
        }


        public  AdminUserDTO GetById(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<AdminUser> bs = new BaseService<AdminUser>(ctx);
                var user = bs.GetAll().ToList()
                    .SingleOrDefault(e => e.Id == id);
                if (user == null)
                {
                    return null;
                }
                return ToDTO(user);
            }
        }
    }
}
