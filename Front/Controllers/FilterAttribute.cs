using Common;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class FilterAttribute : ActionFilterAttribute
    {
        //
        // GET: /Base/

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext); 
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoFilterAttribute), true);
            if (attrs.Length == 1)//有NoFilter属性
            {
                return;//逻辑处理
            }

            //在些判断用户是否已经登录，若未登录，则跳转到登录页面，同时把当前的url作为参数
            long? userId = UserHelper.GetUserId(filterContext.HttpContext);

            if (userId == null)
            {
                filterContext.Result = new RedirectResult("~/Main/Login");
                return;
            }

            return;

        }

    }
    [AttributeUsage(AttributeTargets.Method)]
    public class NoFilterAttribute : Attribute { }

}