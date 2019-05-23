using DAL.IService;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GuidLinesService : IGuidLinesService
    {
        public long[] GetLid(long gid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuidLines> bs = new BaseService<GuidLines>(ctx);
                return bs.GetAll().Where(e => e.Gid == gid).Select(p => p.Lid).ToArray();                
            }
        }

        public long GetGid(long Lid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuidLines> bs = new BaseService<GuidLines>(ctx);
                return bs.GetAll().Where(e => e.Lid == Lid).Select(p => p.Gid).SingleOrDefault();
            }
        }

        public long AddGidLid(long gid, long lid)
        {
            GuidLines GLs = new GuidLines();
            GLs.Gid = gid;
            GLs.Lid = lid;

            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.GuidLines.Add(GLs);
                ctx.SaveChanges();
            }

            return GLs.Id;
        }
    }
}
