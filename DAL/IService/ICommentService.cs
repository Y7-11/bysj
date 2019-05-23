using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface ICommentService : IServiceSupport
    {
        CommentDTO[] GetAll();

        CommentDTO GetById(long id);

        void MarkDeleted(long cid);

        void Update(long id, string phoneNum,
string context, string email);
    }
}
