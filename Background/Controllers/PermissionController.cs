using Background.Models;
using Common;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Background.Controllers
{

    public class PermissionController : Controller
    {
        // GET: Permission

        public IPermissionService PermSvc { get; set; }

        public ActionResult List()
        {
            var perms = PermSvc.GetAll();
            return View(perms);
        }


        public ActionResult Delete(long id)
        {
            PermSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult Delete2(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                PermSvc.MarkDeleted(id);
            }

            return Json(new AjaxResult { status = "ok" });
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PermissionAddNewModel model)
        {
            PermSvc.AddPermission(model.Name, model.Description);
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var perm = PermSvc.GetById(id);
            return View(perm);
        }

        [HttpPost]
        public ActionResult Edit(PermissionEditModel model)
        {
            PermSvc.UpdatePermission(model.Id, model.Name, model.Description);
            return Json(new AjaxResult { status = "ok" });
        }
    }
}