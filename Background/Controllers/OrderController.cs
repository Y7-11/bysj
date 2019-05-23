using Common;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Background.Controllers
{
    public class OrderController : Controller
    {
        public IOrderService OrderSvc { get; set; }
        // GET: Order
        public ActionResult List()
        {
            var order=OrderSvc.GetAll();
            return View(order);
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                OrderSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            OrderSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }
    }
}