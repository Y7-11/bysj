using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IService;
using EFEntity;

namespace DAL
{
    public class GuideUserService : IGuideUserService
    {
        public long[] GetUid(long gid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuideUser> bs = new BaseService<GuideUser>(ctx);
                return bs.GetAll().Where(e => e.GuideId == gid).Select(p => p.Uid).ToArray();
            }
        }

        public long GetGid(long uid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuideUser> bs = new BaseService<GuideUser>(ctx);
                return bs.GetAll().Where(e => e.Uid == uid).Select(p => p.GuideId).SingleOrDefault();
            }
        }


        public bool appointment(long GuideId,long Uid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    GuideUser u = new GuideUser();
                    u.GuideId = GuideId;
                    u.Uid = Uid;
                    ctx.GuideUser.Add(u);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuideUser> bs = new BaseService<GuideUser>(ctx);
                GuideUser gu= bs.GetAll().Where(e => e.GuideId == id).SingleOrDefault();
                gu.IsDeleted = true;
                ctx.SaveChanges();
            }
        }

        public void RecoveryDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuideUser> bs = new BaseService<GuideUser>(ctx);
                GuideUser gu= bs.GetRecoveries().Where(e => e.GuideId == id).SingleOrDefault();
                gu.IsDeleted = false;
                ctx.SaveChanges();
            }
        }

        public long AddGuideUser(long uid,long gid)
        {
            GuideUser g = new GuideUser();
            g.Uid = uid;
            g.GuideId = gid;
            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.GuideUser.Add(g);
                ctx.SaveChanges();
            }

            return g.Id;
        }
    }
}
