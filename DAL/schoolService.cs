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
   public class schoolService:IschoolService
    {
        public schoolDTO[] GetAll()
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                BaseService<school> bs = new BaseService<school>(ctx);
                var school= bs.GetAll().ToList().Select(e=>ToDTO(e));
                return school.ToArray();
            }
        }
        private schoolDTO ToDTO(school r)
        {
            schoolDTO s = new schoolDTO();
            s.schoolname = r.schoolname;
            s.CreateDateTime = r.CreateDateTime;
            return s;
        }

        public void MarkDeleted(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<school> bs = new BaseService<school>(ctx);
                bs.MarkDeleted(id);
            }
        }
        public bool AddSchool(string name)
        {
            using (Dbcontext ctx=new Dbcontext())
            {
                try
                {
                    school s = new school();
                    s.schoolname = name;
                    ctx.school.Add(s);
                    ctx.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
     
            }
        }
    }
}
