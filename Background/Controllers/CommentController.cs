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
    public class CommentController : Controller
    {
        // GET: Comment
        public ICommentService CommentSvc { get; set; }
        public IUserService UserSvc { get; set; }

        public ActionResult List()
        {
            var comments= CommentSvc.GetAll();
            return View(comments);
        }

        public ActionResult show(long uid)
        {
            var users = UserSvc.GetById(uid);
            return View(users);
        }


        public ActionResult Edit(long cid)
        {
            var comments = CommentSvc.GetById(cid);
            return View(comments);
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                CommentSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            CommentSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpPost]
        public ActionResult Edit(CommentEditModel model)
        {
            CommentSvc.Update(model.id, model.phonenum,model.email,model.context);
            return Json(new AjaxResult { status = "ok" });
        }
    }
}