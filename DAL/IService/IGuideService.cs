using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IGuideService : IServiceSupport
    {
        GuideDTO[] Page(int page, out int count, out int totalpage);

        GuideDTO[] GetAll();

        GuideDTO GetById(long id);

        GuideDTO[] GetByshenhe();

        //bool check_Yuyue(long Guide_Id);


        long[] GetLids(long id);

        GuideDTO[] GetTitle(string Title, int page, out int count, out int totalpage);

        void MarkDeleted(long cid);

        GuideDTO[] GetRecoveries();

        void Shenhe(long id);

        long AddGuide(long uid, string school, string schoolnum, string picture, string intro);
        void RecoveryDeleted(long id);

        GuideDTO GetByUid(long uid);

        bool UpdateGuide(long id, string school, string intro);
        bool Addjifen(long gid);
        void changlevel(long level, long gid);
    }
}
