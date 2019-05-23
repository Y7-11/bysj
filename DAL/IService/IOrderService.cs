using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IOrderService:IServiceSupport
    {

        OrderDTO[] GetByIds(long[] Ids);
        OrderDTO[] GetById(long Id);

        long CreateOrder(long tid);
        long[] GetByTids(long[] oid);
        bool CompleteOrder(long oid);
        OrderDTO[] GetAll();
        void MarkDeleted(long id);
    }
}
