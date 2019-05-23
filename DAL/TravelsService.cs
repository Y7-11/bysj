using System;
using System.Data.Entity.Validation;
using DAL.IService;
using System.Linq;
using DTO;
using EFEntity;

namespace DAL
{
    public class TravelsService : ITravelService
    {
        public TravelsDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                return bs.GetAll().ToList().Select(p => ToDTO(p)).ToArray();
                 
            }
        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                bs.MarkDeleted(id);
            }
        }

        public TravelsDTO GetByLid(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                var line = bs.GetAll().SingleOrDefault(e => e.Id == id);
                return ToDTO(line);
            }
        }

        public TravelsDTO[] GetTitle(string Title, int page, out int count, out int totalpage)
        {
            int pagesize = 10;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().Where(e => e.title.Contains(Title)).OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e => ToDTO(e)).ToArray();

            }
        }

        public TravelsDTO[] Page(int page, out int count, out int totalpage)
        {
            int pagesize = 10;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public long? Updatetops(long id)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    BaseService<Travels> bs = new BaseService<Travels>(ctx);
                    bs.GetById(id).tops += 1;
                    ctx.SaveChanges();
                    return bs.GetById(id).tops;
                }

                catch (DbEntityValidationException ex)
                {
                    return null;
                }
         
            }
        }

        public long AddTravel(string title, string context,long? tops,long Uid)
        {
            Travels lines = new Travels();
            lines.title = title;
            lines.content = context;
            lines.tops = tops;
            lines.Uid = Uid;

            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.Travels.Add(lines);
                ctx.SaveChanges();
            }
            return lines.Id;
        }

        public void UpdateTravel(long id, string title, string context, long tops, long Uid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs
                    = new BaseService<Travels>(ctx);
                var line = bs.GetById(id);
                if (line == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的游记");
                }
                line.title = title;
                line.content = context;
                line.tops = tops;
                line.title = title;
                line.Uid = Uid;
                ctx.SaveChanges();
            }
        }


        private TravelsDTO ToDTO(Travels r)
        {
            TravelsDTO dto = new TravelsDTO();
            dto.content = r.content;
            dto.Id = r.Id;
            dto.title = r.title;
            dto.shenhe = r.shenhe;
            dto.tops = r.tops;
            dto.shenhe = r.shenhe;
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<User>bs=new BaseService<User>(ctx);
                var user=bs.GetById(r.Uid);
                if (user!=null)
                {
                    dto.UNickName = user.NickName;
                }
                
            }
         
            dto.CreateDateTime = r.CreateDateTime;
            dto.Id = r.Id;
            return dto;
        }

        public TravelsDTO[] GetByShenhe()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);

                return bs.GetAll().Where(e => e.shenhe == false).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public void Shenhe(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Travels> bs = new BaseService<Travels>(ctx);
                var data = bs.GetById(id);
                data.shenhe = true;
                ctx.SaveChanges();
            }
        }
    }
}
