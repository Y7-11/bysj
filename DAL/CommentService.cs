using DAL.IService;
using DTO;
using EFEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentService:ICommentService
    {
        public CommentDTO[] GetAll()
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Comment> bs = new BaseService<Comment>(ctx);
                return bs.GetAll().Include(h=>h.User).Include(h=>h.Raiders)
                    .ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public CommentDTO GetById(long id)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Comment> bs = new BaseService<Comment>(ctx);
                var comments = bs.GetAll().Include(h=>h.User).Include(h=>h.Raiders).SingleOrDefault(h=>h.Id==id);
                return comments == null ? null : ToDTO(comments);
            }
        }

        public void MarkDeleted(long cid)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Comment> bs = new BaseService<Comment>(ctx);
                bs.MarkDeleted(cid);
            }
        }

        public void Update(long id,  string phoneNum,
string email, string context)
        {
            using (Dbcontext ctx = new Dbcontext())
            {
                BaseService<Comment> bs
                    = new BaseService<Comment>(ctx);

                var comment = bs.GetAll().Include(h=>h.User).Include(h=>h.Raiders).SingleOrDefault(h=>h.Id==id);
                
                if (comment == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的评论");
                }
                comment.User.PhoneNum = phoneNum;
                comment.User.Email = email;
                comment.Context = context;
                ctx.SaveChanges();
            }
        }

        private CommentDTO ToDTO(Comment r)
        {
            CommentDTO dto = new CommentDTO();
            dto.CreateDateTime = r.CreateDateTime;
            dto.Uid = r.User.Id;
            dto.Context = r.Context;
            dto.PhoneNum = r.User.PhoneNum;
            dto.htmlcode = r.Raiders.content;
            dto.title = r.Raiders.title;
            dto.Email = r.User.Email;
            dto.UserName = r.User.NickName;
            dto.Id = r.Id;
            
            return dto;
        }
    }
}
