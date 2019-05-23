using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface ITravelService : IServiceSupport
    {
        TravelsDTO[] Page(int page, out int count, out int totalpage);

        TravelsDTO[] GetAll();

        TravelsDTO[] GetByShenhe();

        void Shenhe(long id);

        long? Updatetops(long id);

        TravelsDTO GetByLid(long id);

        void MarkDeleted(long id);

        long AddTravel(string title, string content, long? tops, long Uid);

        TravelsDTO[] GetTitle(string Title, int page, out int count, out int totalpage);

        void UpdateTravel(long id, string title, string context, long tops, long Uid);
    }
}
