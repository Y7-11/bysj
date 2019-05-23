using Background.Models;
using Common;
using DAL;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Background.Controllers
{

    public class RoleController : Controller
    {
        // GET: Role
        public IRoleService roleservice { get; set; }
        public IPermissionService permservice { get; set; }

        [HttpGet]
        public ActionResult List()
        {
           var roles= roleservice.GetAll();
            return View(roles);
        }

        public ActionResult Delete(long id)
        {
            roleservice.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }
        [HttpGet]
        public ActionResult Add()
        {
            var perms = permservice.GetAll();
            return View(perms);
        }

        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult
                { status = "error", errorMsg = mvchelper.GetValiMsg(ModelState) });
            }

            long roleId = roleservice.AddNew(model.Name);
            permservice.AddPermIds(roleId, model.PermissionIds);
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var role = roleservice.GetById(id);
            var rolePerms = permservice.GetByRoleId(id);
            var allperms = permservice.GetAll();

            RoleEditGetModel model = new RoleEditGetModel();
            model.AllPerms = allperms;
            model.Role = role;
            model.RolePerms = rolePerms;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoleEditModel model)
        {
            roleservice.Update(model.Id, model.Name);
            permservice.UpdatePermIds(model.Id, model.PermissionIds);
            return Json(new AjaxResult { status = "ok" });
        }




        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                roleservice.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpPost]
        public ActionResult Index1()
        {
            return View();
        }
    }
}