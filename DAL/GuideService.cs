using DAL.IService;
using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace DAL
{
    public class GuideService : IGuideService
    {
        public GuideDTO[] Page(int page, out int count, out int totalpage)
        {
            int pagesize = 10;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }
        public GuideDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                return bs.GetAll().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public GuideDTO GetById(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                var user = bs.GetAll().ToList()
                    .SingleOrDefault(e => e.Id == id);
                if (user == null)
                {
                    return null;
                }
                return ToDTO(user);
            }
        }

        private GuideDTO ToDTO(Guide r)
        {
            GuideDTO dto = new GuideDTO();
            dto.CreateDateTime = r.CreateDateTime;
            dto.Id = r.Id;
            dto.intro = r.intro;
            dto.isappointment = r.isappointment;
            dto.level = r.level;
            dto.shenhe = r.shenhe;
            dto.picture = r.picture;
            dto.school = r.school;
            dto.schoolnum = r.schoolnum;
            dto.jifen = r.jifen;
            dto.school = r.school;
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<User> bs = new BaseService<User>(ctx);
                var user= bs.GetById(r.Users_Id);
                if (user!=null)
                {
                    dto.nickname = user.NickName;
                }
               
            }
            dto.Users_Id = r.Users_Id;
            //using (Dbcontext ctx = new Dbcontext())
            //{
            //    BaseService<User> bs = new BaseService<User>(ctx);
            //    var Guide_UserInfo = bs.GetAll().Where(e => e.Id == r.Users_Id).FirstOrDefault();
            //    // dto.NickName = Guide_UserInfo.NickName;
            //}           
            return dto;
        }

        //public bool check_Yuyue(long Guide_Id)
        //{
        //    using (Dbcontext ctx = new Dbcontext())
        //    {
        //        BaseService<Guide> bs = new BaseService<Guide>(ctx);
        //        var guide = bs.GetAll().Where(e => e.Id == Guide_Id).FirstOrDefault();
        //        if (guide.isappointment)
        //            return false;
        //        else
        //            return true;

        //    }
        //}

        //public bool appointment(long Tid, long userid)
        //{
        //    using (Dbcontext ctx = new Dbcontext())
        //    {
        //        try
        //        {

        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            return false;
        //        }

        //        return true;
        //    }

        //}

        public long AddGuide(long uid, string school,string schoolnum, string picture,string intro)
        {
            Guide g = new Guide();
            g.intro = intro;
            g.isappointment = false;
            g.level = 1;
            g.school = school;
            g.schoolnum = schoolnum;
            g.picture = picture;
            g.Users_Id = uid;
            using (Dbcontext ctx = new Dbcontext())
            {
                ctx.Guide.Add(g);
                ctx.SaveChanges();
            }

            return g.Id;
        }

        public long[] GetLids(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<GuidLines> bs = new BaseService<GuidLines>(ctx);
                var lines = bs.GetAll().Where(e => e.Gid == id).ToList().Select(e => e.Lid).ToArray();
                return lines;

            }
        }

        public GuideDTO[] GetTitle(string Title, int page, out int count, out int totalpage)
        {
            int pagesize = 10;
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                count = bs.GetAll().Count();
                totalpage = count % pagesize == 0 ? count / pagesize : (count / pagesize) + 1;
                return bs.GetAll().Where(e => e.intro.Contains(Title)).OrderBy(p => p.Id).Skip(((int)page - 1) * pagesize)
                    .Take(pagesize).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public void MarkDeleted(long cid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                bs.MarkDeleted(cid);
            }
        }

        public GuideDTO[] GetRecoveries()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                return bs.GetRecoveries().ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public void RecoveryDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                bs.RecoveryDeleted(id);
            }
        }

        public GuideDTO GetByUid(long uid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                var guide = bs.GetAll().SingleOrDefault(e => e.Users_Id == uid);
                return ToDTO(guide);
            }
        }


        public bool UpdateGuide(long id, string school, string intro)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                try
                {
                    BaseService<Guide> bs
                        = new BaseService<Guide>(ctx);
                    BaseService<User> userbs = new BaseService<User>(ctx);
                    var guide = bs.GetById(id);
                    var user = userbs.GetById(guide.Users_Id);
                    if (guide == null)
                    {
                        throw new ArgumentException("找不到id=" + id + "的导游");
                    }

                    if (user == null)
                    {
                        throw new ArgumentException("找不到id=" + guide.Users_Id + "的用户");
                    }
                    guide.school = school;
                    guide.intro = intro;
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        public GuideDTO[] GetByshenhe()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);

                return bs.GetAll().Where(e => e.shenhe == false).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public void Shenhe(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                var data= bs.GetById(id);
                data.shenhe = true;
                ctx.SaveChanges();
            }
        }

        public bool Addjifen(long gid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    BaseService<Guide> bs = new BaseService<Guide>(ctx);
                    var guide = bs.GetById(gid);
                    guide.jifen += 10;
                    ctx.SaveChanges();
                    return true;
                }
                catch 
                {
                    return false;
                }
            
            }
        }

        public void changlevel(long level,long gid)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<Guide> bs = new BaseService<Guide>(ctx);
                bs.GetById(gid).level=(int)level;
                ctx.SaveChanges();
            }
        }

    }
}
