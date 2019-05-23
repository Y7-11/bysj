using DAL.IService;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserTravelsService : IUserTravelsService
    {
        public bool ClickTops(long Uid, long Tid)
        {

            using (Dbcontext ctx = new Dbcontext())
            {
                UserTravels ut = new UserTravels();
                ut.UId = Uid;
                ut.TId = Tid;
                ctx.UserTravels.Add(ut);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool IsClick(long Uid,long tid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<UserTravels>bs=new BaseService<UserTravels>(ctx);
                var usertravels= bs.GetAll().Where(e => e.UId == Uid).ToList();
                foreach (var item in usertravels)
                {
                    if (item.TId==tid)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
