using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IGuideUserService:IServiceSupport
    {
        bool appointment(long GuideId, long Uid);

        long GetGid(long uid);
        void MarkDeleted(long id);

        void RecoveryDeleted(long id);

        long[] GetUid(long gid);
    }
}
