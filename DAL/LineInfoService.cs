using DAL.IService;
using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LineInfoService: ILineInfoService
    {
        public long AddLineInfo(long lid, string Address, string ScenicSpot, string SpotInfo, string Apartment, string Level, string Facilities,string attention,long tag)
        {
            LinesInfo linesInfo = new LinesInfo();
            linesInfo.Lid = lid;
            linesInfo.Address = Address;
            linesInfo.ScenicSpot = ScenicSpot;
            linesInfo.SpotInfo = SpotInfo;
            linesInfo.Apartment = Apartment;
            linesInfo.Level = Level;
            linesInfo.Facilities = Facilities;
            linesInfo.attention = attention;
            linesInfo.tag = tag;

            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.LinesInfo.Add(linesInfo);
                ctx.SaveChanges();
            }

            return linesInfo.Id;
        }

        public LinesInfoDTO[] GetById(long id)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<LinesInfo> bs = new BaseService<LinesInfo>(ctx);
                return bs.GetAll().Where(e => e.Lid == id).ToList()
                    .Select(e=>ToDTO(e)).ToArray();
            }
        }
        private LinesInfoDTO ToDTO(LinesInfo r)
        {
            LinesInfoDTO dto = new LinesInfoDTO();
            dto.CreateDateTime = r.CreateDateTime;
            dto.Level = r.Level;
            dto.Address = r.Address;
            dto.Apartment = r.Apartment;
            dto.Facilities = r.Facilities;
            dto.ScenicSpot = r.ScenicSpot;
            dto.SpotInfo = r.SpotInfo;
            dto.tag = r.tag;
            dto.Id = r.Id;
            dto.attention = r.attention;
            return dto;
        }
    }
}
