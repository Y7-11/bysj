using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface ILineService:IServiceSupport
    {
        LineDTO[] Page(int page, out int count, out int totalpage);

        LineDTO[] GetAll();

        LineDTO[] GetByFour();

        LineDTO GetByLid(long id);

        void MarkDeleted(long id);
        LineDTO[] GetByLids(long[] ids);

        bool AddNumOfPeople(long tid);

        void UpdateUser(long id, string Province, string city, string intro, string title, long PastPrice,
            double discount);
        long AddLine(string Province, string city, string intro, string title, long PastPrice, float discount);


        LineDTO[] GetTitle(string Title, int page, out int count, out int totalpage);
    }
}
