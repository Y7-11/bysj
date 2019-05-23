using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IGuidLinesService:IServiceSupport
    {
        long[] GetLid(long gid);

        long GetGid(long Lid);

        long AddGidLid(long gid, long lid);
    }
}
