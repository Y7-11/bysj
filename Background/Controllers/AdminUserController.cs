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

    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public IAdminUserService adminuserSvc { get; set; }
        public IRoleService roleService { get; set; }

        public ActionResult List()
        {
            var user = adminuserSvc.GetAll();
            return View(user);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var roles = roleService.GetAll();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Add(AdminUserAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            //服务器端的校验必不可少
            bool exists = adminuserSvc.GetByPhoneNum(model.PhoneNum) != null;
            if (exists)
            {
                return Json(new AjaxResult
                {
                    status = "error",
                    errorMsg = "手机号已经存在"
                });
            }

            long userId = adminuserSvc.AddAdminUser(model.Name,
                model.PhoneNum, model.Password, model.Email);
            roleService.AddRoleIds(userId, model.RoleIds);
            return Json(new AjaxResult { status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var adminUser = adminuserSvc.GetById(id);
            if (adminUser == null)
            {
                //不要忘了第二个参数的(object) 
                //如果视图在当前文件夹下没有找到，则去Shared下去找
                //一个Error视图，大家共用
                //return Redirect();
                return View("Error", (object)"id指定的操作员不存在");
            }

            var roles = roleService.GetAll();
            long[] RolesIds = roleService.GetByAdminUserId(id);//获得用户拥有的角色

            AdminUserEditViewModel model = new AdminUserEditViewModel();
            model.UserRoleIds = RolesIds;
            model.AdminUser = adminUser;
            model.Roles = roles;
            return View(model);
        }

        /// <summary>
        /// 检查手机号是否已经存在
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public ActionResult CheckPhoneNum(string phoneNum, long? userId)
        {
            var user = adminuserSvc.GetByPhoneNum(phoneNum);
            bool isOK = false;
            //如果没有给userId，则说明是“插入”，只要检查是不是存在这个手机号
            if (userId == null)
            {
                isOK = (user == null);
            }
            else//如果有userId，则说明是修改，则要把自己排除在外
            {
                isOK = (user == null || user.Id == userId);
            }
            return Json(new AjaxResult { status = isOK ? "ok" : "exists" });
        }

        public ActionResult Delete(long id)
        {
            adminuserSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }


        [HttpPost]
        public ActionResult Edit(AdminUserEditModel model)
        {
            //修改了UpdateAdminUser方法的实现：当然password为空，不更新Password

            adminuserSvc.UpdateAdminUser(model.Id, model.Name,
                model.PhoneNum, model.Password, model.Email);
            roleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { status = "ok" });
        }


        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                adminuserSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
    }
}