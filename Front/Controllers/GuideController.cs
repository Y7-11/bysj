using Common;
using DAL;
using DAL.IService;
using DTO;
using Front.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Web.Mvc;
using ViewModel;
using Newtonsoft.Json;

namespace Front.Controllers
{
    [Filter]
    public class GuideController : Controller
    {
        public IGuideService GuideSvc { get; set; }
        public ILineService LineSvc { get; set; }
        public IUserService UserSvc { get; set; }
        public IschoolService schoolSvc { get; set; }
        public IGuideUserService GuideUserSvc { get; set; }
        public IOrderService OrderSvc { get; set; }

        /// <summary>
        /// 导游列表页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            int count = 0;
            int totalpage = 0;
            var guide = GuideSvc.Page(page, out count, out totalpage);
            ViewData["totalpage"] = totalpage;
            ViewBag.nickname = user.NickName;
            ViewBag.phonenum = user.PhoneNum;
            return View(guide);
        }
        /// <summary>
        /// 搜索导游列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string search, int page = 1)
        {
            int count = 0;
            int totalpage = 0;
            var guide = GuideSvc.GetTitle(search, page, out count, out totalpage);
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            ViewBag.nickname = user.NickName;
            ViewBag.phonenum = user.PhoneNum;
            ViewData["totalpage"] = totalpage;
            return View(guide);
        }
        /// <summary>
        /// 导游列表详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Info(int id)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            var guide = GuideSvc.GetById(id);
            if (guide == null)
            {
                return View("Error", (object)"id指定的导游不存在");
            }
            long[] lids = GuideSvc.GetLids((long)id);//获得guide为1的所有路线id

            var lines = LineSvc.GetByLids(lids);

            ViewBag.nickname = user.NickName;
            ViewBag.phonenum = user.PhoneNum;
            GuidInfoViewModel model = new GuidInfoViewModel();
            model.guide = guide;
            model.lines = lines;
            return View(model);

        }

        public ActionResult RegisterGuide()
        {
            var school=schoolSvc.GetAll();
            return View(school);
        }

        [HttpPost]
        public ActionResult ToBeGuide()
        {
            GuideDTO model = new GuideDTO();
            string school = Request["schoolname"];
            string schoolnum = Request["schoolnum"];
            string intro = Request["intro"];
            HttpPostedFileBase file = Request.Files["upfile"];
            model.intro = intro;
            model.school = school;
            if (file.ContentLength > 0)
            {
                file.SaveAs(Server.MapPath("~/images/head/") + System.IO.Path.GetFileName(file.FileName));
            }
            model.picture = "/images/head/" + System.IO.Path.GetFileName(file.FileName);
       
            long id = new GuideService().AddGuide((long)Session["LoginUserId"], school,schoolnum, model.picture, intro);
            long[] role = new long[1];
            role[0] = 6;
            new UserRoleService().AddUserRole((long)Session["LoginUserId"], role);
            if (new GuideUserService().AddGuideUser((long)Session["LoginUserId"], id) > 0)
            {
                return Redirect("/Main/Index");
            }
            else
            {
                return Content("<script>alert('注册失败');history.go(-1);</script>");
            }
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
                    }
                }
                return Redirect("/Main/Center");
            }
            catch 
            {
                return Redirect("/Main/Center");
            }
           
        }

        public long GetLevel(long gid)
        {
            var guide=GuideSvc.GetById(gid);
            if (guide.jifen>=1000)
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