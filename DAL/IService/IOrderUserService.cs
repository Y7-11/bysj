using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IOrderUserService:IServiceSupport
    {
        long[] GetById(long userId);

        bool appointment(long uid, long oid);
    }
}
