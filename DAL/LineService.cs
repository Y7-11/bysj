using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DTO;
using EFEntity;

namespace DAL
{
    public class LineService : ILineService
    {
        public LineDTO[] Page(int page, out int count, out int totalpage)
        {
            int pagesize = 10;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public LineDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                return bs.GetAll().ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        public LineDTO[] GetByFour()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                return bs.GetAll().OrderByDescending(p => p.NumOfPeople).Take(4).ToList().Select(p => ToDTO(p)).ToArray();
            }
        }

        public LineDTO GetByLid(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                var line= bs.GetAll().SingleOrDefault(e => e.Id == id);
                return ToDTO(line);
            }
        }


        public LineDTO[] GetByLids(long[] ids)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                List<LineDTO> lines = new List<LineDTO>();
                BaseService<Line> bs = new BaseService<Line>(ctx);
                foreach (var id in ids)
                {
                    var line = bs.GetAll().Where(e => e.Id == id).ToList().Select(p => ToDTO(p));
                    lines.AddRange(line);
                }

                return lines.ToArray();
            }
        }
        public long AddLine(string Province, string city, string intro, string title, long PastPrice, float discount)
        {
            Line lines = new Line();
            lines.Province = Province;
            lines.city = city;
            lines.intro = intro;
            lines.title = title;
            lines.NowPrice = (PastPrice * (discount * 0.1));
            lines.PastPrice = PastPrice;
            lines.discount = discount;

            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.Line.Add(lines);
                ctx.SaveChanges();
            }
            return lines.Id;
        }

        public bool AddNumOfPeople(long tid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    BaseService<Line> bs = new BaseService<Line>(ctx);
                    var line = bs.GetById(tid);
                    line.NumOfPeople += 1;
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
             
            }
        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public void UpdateUser(long id,string Province, string city, string intro, string title, long PastPrice,
            double discount)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs
                    = new BaseService<Line>(ctx);
                var line = bs.GetById(id);
                if (line == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的路线");
                }
                line.Province = Province;
                line.city = city;
                line.intro = intro;
                line.title = title;
                line.PastPrice = PastPrice;
                line.discount = discount;
                ctx.SaveChanges();
            }
        }

        private LineDTO ToDTO(Line r)
        {
            LineDTO dto = new LineDTO();
            dto.discount = r.discount;
            dto.intro = r.intro;
            dto.Id = r.Id;
            dto.title = r.title;
            dto.Province = r.Province;
            dto.city = r.city;
            dto.NowPrice = r.PastPrice*(r.discount*0.01);
            dto.NumOfComment = r.NumOfComment;
            dto.NumOfPeople = r.NumOfPeople;
            dto.CreateDateTime = r.CreateDateTime;
            dto.PastPrice = r.PastPrice;
            return dto;
        }

        public LineDTO[] GetTitle(string Title, int page, out int count, out int totalpage)
        {
            int pagesize = 10;            
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Line> bs = new BaseService<Line>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().Where(e => e.title.Contains(Title) || e.intro.Contains(Title)).OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e=>ToDTO(e)).ToArray();
                
            }
        }
    }
}
