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

    public class UserController : Controller
    {
        public IUserService UserSvc { get; set; }
        public IUserRoleService UserRoleSvc { get; set; }
        public IRoleService roleService { get; set; }
        // GET: User
        public ActionResult List()
        {
            var users = UserSvc.GetAll();
            return View(users);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user=  UserSvc.GetById(id);
            var roles = roleService.GetAll();
            var userRoles = roleService.GetByUserId(id);//获得用户拥有的角色
            UserEditViewModel model=new UserEditViewModel();
            model.User = user;
            model.UserRoleIds = userRoles.Select(r => r.Id).ToArray();
            model.Roles = roles;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserEditModel model)
        {
            UserSvc.UpdateUser(model.id, model.nickname, model.phonenum
                , model.gender, model.password, model.email);
            UserRoleSvc.AddUserRole(model.id, model.RoleIds);
            return Json(new AjaxResult { status = "ok" });
        }


        [HttpGet]
        public ActionResult Add()
        {
            var roles = roleService.GetAll();
            return View(roles);
        }

        public ActionResult DelList()
        {
            var users = UserSvc.GetRecoveries();
            return View(users);
        }



        public ActionResult BatchRecovery(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                UserSvc.RecoveryDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });

        }

        [HttpPost]
        public ActionResult Add(UserAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            //服务器端的校验必不可少
            bool exists = UserSvc.GetByPhoneNum(model.phonenum) != null;
            if (exists)
            {
                return Json(new AjaxResult
                {
                    status = "error",
                    errorMsg = "手机号已经存在"
                });
            }

            long userId = UserSvc.AddUser(model.account,model.nickname,
                model.phonenum, model.gender,model.password,model.email);
            UserRoleSvc.AddUserRole(userId, model.RoleIds);
            //roleService.AddRoleIds(userId, model.RoleIds);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            UserSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult Recovery(long id)
        {
            UserSvc.RecoveryDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                UserSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
    }
}