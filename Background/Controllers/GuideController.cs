using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Background.Models;
using Common;
using DAL.IService;
using DTO;
using System.Transactions;
using ViewModel;

namespace Background.Controllers
{

    public class GuideController : Controller
    {
        public IGuideService GuideSvc { get; set; }
        public IGuideUserService GuideUserSvc { get; set; }
        public IUserService UserSvc { get; set; }
        public IOrderService OrderSvc { get; set; }
        //
        // GET: /Guide/
        public ActionResult List()
        {
            var guide = GuideSvc.GetAll().Where(e=>e.shenhe==true).ToArray();
            List<UserDTO> list = new List<UserDTO>();
            foreach (var item in guide)
            {
                var userid = item.Users_Id;
                var user = UserSvc.GetById(userid);
                list.Add(user);
            }
            GuideListViewModel model = new GuideListViewModel();
            model.guide = guide;
            model.users = list.ToArray();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(GuideAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            //服务器端的校验必不可少
            bool exists = UserSvc.GetByAccount(model.account) != null;
            if (!exists)
            {
                return Json(new AjaxResult
                {
                    status = "error",
                    errorMsg = "该游客不存在"
                });
            }

            using (TransactionScope ts = new TransactionScope())
            {
                var user = UserSvc.GetByAccount(model.account);
                long guideId = GuideSvc.AddGuide(user.Id, model.school,model.schoolnum,model.picture , model.intro);
                bool isok = GuideUserSvc.appointment(guideId, user.Id);
                if (isok)
                {
                    ts.Complete();
                    return Json(new AjaxResult { status = "ok" });
                }
                else
                {
                    return Json(new AjaxResult { status = "error" });
                }
            }

            //roleService.AddRoleIds(userId, model.RoleIds);

        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var guide = GuideSvc.GetById(id);
            var user = UserSvc.GetById(guide.Users_Id);
            GuideEditViewModel model = new GuideEditViewModel();
            model.guide = guide;
            model.Account = user.Account;
            model.email = user.Email;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(GuideEditModel model)
        {
            bool isok = GuideSvc.UpdateGuide(model.id, model.school, model.intro);
            if (isok)
            {
                return Json(new AjaxResult { status = "ok" });
            }
            return Json(new AjaxResult { status = "error" });
        }

        public ActionResult DelList()
        {
            var guide = GuideSvc.GetRecoveries();
            List<UserDTO> list = new List<UserDTO>();
            foreach (var item in guide)
            {
                var userid = item.Users_Id;
                var user = UserSvc.GetById(userid);
                list.Add(user);
            }
            GuideListViewModel model = new GuideListViewModel();
            model.guide = guide;
            model.users = list.ToArray();
            return View(model);
        }
        public ActionResult Delete(long id)
        {
            GuideSvc.MarkDeleted(id);
            GuideUserSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                GuideSvc.MarkDeleted(id);
                GuideUserSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult BatchRecovery(long[] selectedIds)
        {
            foreach (long id in selectedIds)
            {
                GuideSvc.RecoveryDeleted(id);
                GuideUserSvc.RecoveryDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });

        }

        public ActionResult Recovery(long id)
        {
            GuideSvc.RecoveryDeleted(id);
            GuideUserSvc.RecoveryDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult CompleteOrder(long id)
        {
            try
            {
                var userid = Common.UserHelper.GetUserId(HttpContext);
                var isok = OrderSvc.CompleteOrder(id);
                if (isok)
                {
                    var guide = GuideSvc.GetByUid((long)userid);
                    var addjf = GuideSvc.Addjifen(guide.Id);
                    if (addjf)
                    {
                        var level = GetLevel(guide.Id);
                        GuideSvc.changlevel(level, guide.Id);
                        return Json(new AjaxResult { status = "ok" });
                    }
                }
                return Json(new AjaxResult { status = "error" });
            }
            catch
            {
                return Json(new AjaxResult { status = "error" });
            }

        }
        public long GetLevel(long gid)
        {
            var guide = GuideSvc.GetById(gid);
            if (guide.jifen >= 1000)
            {
                return 5;
            }
            else if (guide.jifen >= 600)
            {
                return 4;
            }
            else if (guide.jifen >= 400)
            {
                return 3;
            }
            else if (guide.jifen >= 200)
            {
                return 2;
            }
            return 1;
        }
    }
}