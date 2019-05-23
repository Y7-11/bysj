using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Background.Models;
using Common;
using DAL.IService;

namespace Background.Controllers
{

    public class TravelController : Controller
    {
        public ILineService LineSvc { get; set; }
        public IUserService UserSvc { get; set; }
        public ITravelService TravelSvc { get; set; }
        //
        // GET: /Travel/
        public ActionResult LineList()
        {
            var lines= LineSvc.GetAll();
            return View(lines);
        }

        public ActionResult TravelList()
        {
            var travel = TravelSvc.GetAll().Where(e=>e.shenhe==true).ToArray();
            return View(travel);
        }

        public ActionResult Delete(long id)
        {
            LineSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }
        public ActionResult TravelDelete(long id)
        {
            TravelSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }


        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                LineSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
        public ActionResult BatchDeleteTravel(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                TravelSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
        

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(LineAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            //服务器端的校验必不可少
            //bool exists = LineSvc.GetByLid(model.id) != null;
            //if (exists)
            //{
            //    return Json(new AjaxResult
            //    {
            //        status = "error",
            //        errorMsg = "该路线已经存在"
            //    });
            //}

            long userId = LineSvc.AddLine(model.Province, model.city,
                model.intro, model.title, model.PastPrice, model.discount);

            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult TravelAdd()
        {
            var user = UserSvc.GetAll();
            return View(user);
        }

        [HttpPost]
        public ActionResult TravelAdd(TravelAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            //服务器端的校验必不可少
            //bool exists = LineSvc.GetByLid(model.id) != null;
            //if (exists)
            //{
            //    return Json(new AjaxResult
            //    {
            //        status = "error",
            //        errorMsg = "该路线已经存在"
            //    });
            //}

            long userId = TravelSvc.AddTravel(model.title, model.context,
                model.tops, model.Uid);

            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var Line = LineSvc.GetByLid(id);
            return View(Line);
        }

        [HttpPost]
        public ActionResult Edit(LineEditModel model)
        {
            LineSvc.UpdateUser(model.id, model.Province, model.city, model.intro
                , model.title, model.PastPrice, model.discount);
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult TravelEdit(long id)
        {
            var Line = TravelSvc.GetByLid(id);
            return View(Line);
        }

        [HttpPost]
        public ActionResult TravelEdit(TravelEditModel model)
        {
            TravelSvc.UpdateTravel(model.id, model.title, model.context, model.tops
                , model.Uid);
            return Json(new AjaxResult { status = "ok" });
        }

	}
}