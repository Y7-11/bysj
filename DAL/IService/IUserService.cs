using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IUserService:IServiceSupport
    {
        bool CheckLogin(string account, string password);

        UserDTO GetByPhoneNum(string phoneNum);

        UserDTO GetByAccount(string account);

        UserDTO GetById(long id);

        UserDTO[] GetRecoveries();

        UserDTO[] GetAll();

        void MarkDeleted(long id);

        void RecoveryDeleted(long id);

        long AddUser(string nickename, string phoneNum, string gender, string password, string address, string email);

        bool UpdateUser(long id, string nickname, string phoneNum, string gender,
            string password, string email);

    }
}
