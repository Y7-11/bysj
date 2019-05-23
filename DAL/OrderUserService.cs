using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using EFEntity;

namespace DAL
{
    public class OrderUserService : IOrderUserService
    {
        public long[] GetById(long userId)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<OrderUser> bs = new BaseService<OrderUser>(ctx);
                long[] ids = bs.GetAll().Where(p => p.Uid == userId).Select(s=>s.Oid).ToArray();
                return ids;
            }
        }

        public bool appointment(long uid, long oid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    OrderUser ou = new OrderUser();
                    ou.Uid = uid;
                    ou.Oid = oid;
                    ctx.OrderUser.Add(ou);
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
               
            }
        }


    }
}
