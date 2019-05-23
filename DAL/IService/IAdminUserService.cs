using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IAdminUserService:IServiceSupport
    {
        bool CheckLogin(string phoneNum, string password);

        AdminUserDTO[] GetAll();

        void UpdateAdminUser(long id, string name, string phoneNum,
string password, string email);

        long AddAdminUser(string name, string phoneNum, string password, string email);

        void MarkDeleted(long adminUserId);

        AdminUserDTO GetByPhoneNum(string phoneNum);

        AdminUserDTO GetById(long id);

    }
}
