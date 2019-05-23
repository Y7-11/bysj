using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface ILineInfoService:IServiceSupport
    {
        LinesInfoDTO[] GetById(long id);

        long AddLineInfo(long lid, string Address, string ScenicSpot, string SpotInfo, string Apartment, string Level, string Facilities, string attention, long tag);
    }
}
