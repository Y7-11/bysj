using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class UserHelper
    {
        public static long? GetUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginUserId"];
        }

        public static long? GetAdminUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginAdminUserId"];
        }
    }
}
