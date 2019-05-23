using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IUserTravelsService:IServiceSupport
    {
        bool ClickTops(long Uid, long Tid);
        bool IsClick(long Uid, long tid);
    }
}
