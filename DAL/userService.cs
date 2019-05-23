using Common;
using DAL.IService;
using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class UserService : IUserService
    {
        public bool CheckLogin(string account, string password)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                var user = bs.GetAll().SingleOrDefault(e => e.Account ==account);
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

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public void RecoveryDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                bs.RecoveryDeleted(id);
            }
        }

        public long AddUser(string account, string nickename, string phoneNum, string gender, string password, string email)
        {
            User user = new User();
            user.Email = email;
            user.NickName = nickename;
            user.PhoneNum = phoneNum;
            user.Gender = gender;
            user.Account = account;
            var salt = commonhelper.CreateVerifyCode(4);
            user.PasswordSalt = salt;
            string pwdHash = commonhelper.CalcMD5(salt + password);
            user.PasswordHash = pwdHash;


            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                bool exists = bs.GetAll().Any(e => e.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已经存在" + phoneNum);
                }           
                ctx.User.Add(user);
                ctx.SaveChanges();
            }
            return user.Id;
        }

        public int checkpassword(string pwd)
        {
            Regex d = new Regex("/[^a-zA-Z0-9_]/g");
            Regex z = new Regex("/[a-zA-Z]/g");
            //Regex x = new Regex("/[0-9]/g");
            if (d.IsMatch(pwd))
            {
                return 3;
            }
            if (d.IsMatch(pwd))
            {
                return 2;
            }

            return 1;
        }

        //重载了上面的AddUser方法，传进来一个UserDTO的对象，然后对上面的方法进行少量修改
        public long AddUser(UserDTO model)
        {
            User user = new User();
            user.Email = model.Email;
            user.NickName = model.NickName;
            user.PhoneNum = model.PhoneNum;
            user.Gender = model.Gender;
            user.Account = model.Account;
            var salt = commonhelper.CreateVerifyCode(4);
            user.PasswordSalt = salt;
            string pwdHash = commonhelper.CalcMD5(salt + model.Password);
            user.PasswordHash = pwdHash;
            var level=checkpassword(model.Password);
            user.level = level;

            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                bool exists = bs.GetAll().Any(e => e.Account == model.Account);
                if (exists)
                {
                    return -123;//这里要注意以下，上面的原方法中是抛出一个异常，但是如果是发布之后的话，异常是要进行处理掉的，不然会出现黄屏等问题
                                //我这里就暂时定义了 -123的意思就是存在相同的账号,这个是可以随意修改，但是最好有一定规则
                }
                ctx.User.Add(user);
                ctx.SaveChanges();
            }
            return user.Id;
        }

        public bool UpdateUser(long id, string nickname, string phoneNum, string gender,
string password, string email)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs
                    = new BaseService<User>(ctx);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的用户");
                }
                user.NickName = nickname;
                user.PhoneNum = phoneNum;
                user.Gender = gender;
                user.Email = email;
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash =
                    commonhelper.CalcMD5(user.PasswordSalt + password);
                }
                ctx.SaveChanges();
                return true;
            }
        }

        public UserDTO[] GetRecoveries()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                return bs.GetRecoveries().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public UserDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                var user = bs.GetAll().SingleOrDefault(e => e.PhoneNum == phoneNum);
                return user == null ? null : ToDTO(user);
            }
        }

        public UserDTO GetByAccount(string account)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                var user = bs.GetAll().SingleOrDefault(e => e.Account == account);
                return user == null ? null : ToDTO(user);
            }
        }


        private UserDTO ToDTO(User u)
        {
            UserDTO dto = new UserDTO();
            dto.CreateDateTime = u.CreateDateTime;
            dto.Email = u.Email;
            dto.Level = u.level;
            dto.Account = u.Account;
            dto.NickName = u.NickName;
            dto.Gender = u.Gender;
            dto.Id = u.Id;
            dto.PhoneNum = u.PhoneNum;
            return dto;
        }


        public UserDTO GetById(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                var user = bs.GetById(id);
                return user == null ? null : ToDTO(user);
            }
        }

        public bool IsGuide(long id)
        {
            BaseService<GuideUser> baseService = new BaseService<GuideUser>(new Dbcontext());
            bool exists = baseService.GetAll().Any(e => e.Uid == id);
            return exists;
        }

    }
}
