using Common;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Background.Controllers
{
    public class schoolController : Controller
    {
        public IschoolService schoolSvc { get; set; }
        // GET: school
        public ActionResult List()
        {
           var school= schoolSvc.GetAll();
            return View(school);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string schoolname)
        {
            bool isok=schoolSvc.AddSchool(schoolname);
            if (isok)
            {
                return Json(new AjaxResult { status = "ok" });
            }
            return Json(new AjaxResult { status = "error" });
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                schoolSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
    }
}