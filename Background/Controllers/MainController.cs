using Background.Models;
using CaptchaGen;
using Common;
using DAL;
using DAL.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;

namespace Background.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public IAdminUserService adminuserService { get; set; }
        public IAdminUserRoleService AdminuserRoleSvc { get; set; }
        public IRoleService RoleSvc { get; set; }
        public ActionResult Index()
        {
            long? userId = UserHelper.GetAdminUserId(HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            var user = adminuserService.GetById(userId.Value);
            return View(user);
        }


        public ActionResult welcome()
        {
            ViewBag.now = DateTime.Now;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public bool IsAdministrator(long adminuserid)
        {
            var roleids = AdminuserRoleSvc.GetRoleId(adminuserid);
            var r = RoleSvc.GetByRoleId(roleids);
            foreach (var item in r)
            {
                if (item.Name == "超级管理员")
                {
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var adminuserid = adminuserService.GetByPhoneNum(model.PhoneNum).Id;
            var islogin = IsAdministrator(adminuserid);
            if (!islogin)
            {
                return Json(new AjaxResult { status = "error", errorMsg = "没有登陆权限" });
            }

            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { status = "error", errorMsg = mvchelper.GetValiMsg(ModelState) });
            }

            if (model.VerifyCode != (string)TempData["verifyCode"])
            {
                return Json(new AjaxResult { status = "error", errorMsg = "验证码错误" });
            }
            bool result = adminuserService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                Session["LoginAdminUserId"] = adminuserid;
                return Json(new AjaxResult { status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { status = "error", errorMsg = "用户名或者密码错误" });
            }

        }

        public ActionResult CreateVerifyCode()
        {
            string verifycode = commonhelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifycode;
            /* using (MemoryStream ms=ImageFactory.GenerateImage(verifycode,60,100,20,60))
             {
                 return File(ms, "image/jpeg");
             }*/
            MemoryStream ms = ImageFactory.GenerateImage(verifycode, 60, 100, 20, 10);
            return File(ms, "image/jpeg");
        }
    }
}