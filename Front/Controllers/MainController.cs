using CaptchaGen;
using Common;
using DAL;
using DAL.IService;
using DTO;
using Front.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Front.Controllers
{
    [Filter]
    public class MainController : Controller
    {
        public IUserService UserSvc { get; set; }
        public IOrderUserService OrderUserSvc { get; set; }
        public IOrderService OrderSvc { get; set; }
        public ILineService LineSvc { get; set; }
        public IUserRoleService UserRoleSvc { get; set; }
        public IGuideService GuideSvc { get; set; }
        public IGuidLinesService GuidLinesSvc { get; set; }
        public IGuideUserService GuideUserSvc { get; set; }
        public IschoolService schoolSvc { get; set; }
        public ITravelService TravelSvc { get; set; }
        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NoFilter]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [NoFilter]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { status = "error", errorMsg = mvchelper.GetValiMsg(ModelState) });
            }

            if (model.VerifyCode != (string)TempData["verifyCode"])
            {
                return Json(new AjaxResult { status = "error", errorMsg = "验证码错误" });
            }
            bool result = UserSvc.CheckLogin(model.Account, model.Password);
            if (result)
            {
                try
                {
                    Session["LoginUserId"] = UserSvc.GetByAccount(model.Account).Id;
                    return Json(new AjaxResult { status = "ok" });
                }
                catch (Exception e)
                {
                    return Json(new AjaxResult { status = "error", errorMsg = e.Message });
                }
            }
            else
            {
                return Json(new AjaxResult { status = "error", errorMsg = "用户名或者密码错误" });
            }

        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            var Lines = LineSvc.GetByFour();
            var model = new LinesAndTime();
            model.Lines = Lines;
            model.Now = DateTime.Now;
            ViewBag.phonenum = user.PhoneNum;

            return View(model);

        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <returns></returns>
        [NoFilter]
        public ActionResult CreateVerifyCode()
        {
            string verifycode = commonhelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifycode;
            /* using (MemoryStream ms=ImageFactory.GenerateImage(verifycode,60,100,20,60))
             {
                 return File(ms, "image/jpeg");
             }*/
            MemoryStream ms = ImageFactory.GenerateImage(verifycode, 50, 70, 20, 0);
            return File(ms, "image/jpeg");
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult Center()
        {
            var user = UserSvc.GetById((long)UserHelper.GetUserId(HttpContext));
            var ids = OrderUserSvc.GetById(user.Id).LastOrDefault();
            var orders = OrderSvc.GetById(ids);
            UserOrderInfo info = new UserOrderInfo();
            info.user = user;
            info.orders = orders;
            ViewBag.phonenum = user.PhoneNum;
            ViewBag.userid = UserHelper.GetUserId(HttpContext);
            ViewBag.IsGuide = new UserService().IsGuide((long)Session["LoginUserId"]);
            return View(info);
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder(long userid)
        {
            var user = UserSvc.GetById((long)userid);
            var ids = OrderUserSvc.GetById(user.Id);
            var orders = OrderSvc.GetByIds(ids);
            MyOrderViewModel model = new MyOrderViewModel();
            model.orders = orders;
            var guide=GuideSvc.GetByUid(userid);
            model.isguide = false;
            if (guide!=null)
            {
                model.isguide = true;
            }
 
            ViewBag.phonenum = user.PhoneNum;
            return View(model);
        }

        /// <summary>
        /// 我的路线（针对导游）
        /// </summary>
        /// <returns></returns>
        public ActionResult MyLines(long userid)
        {
            var user = UserSvc.GetById((long)userid);//user表
            var roleids = UserRoleSvc.GetById(user.Id);
            int i = Array.IndexOf(roleids, 6);
            if (i==-1)
            {
                return  Redirect("~/Guide/RegisterGuide");
            }
            else
            {
                var guide = GuideSvc.GetByUid(user.Id);
                var lids = GuidLinesSvc.GetLid(guide.Id);
                var lines = LineSvc.GetByLids(lids);

                var uids = GuideUserSvc.GetUid(guide.Id);
                List<UserDTO> users=new List<UserDTO>();
                //List<OrderUserDTO> ou = new List<OrderUserDTO>();
                foreach (var id in uids)
                {
                    users.Add(UserSvc.GetById(id));
                }
                LinesAndUser model = new LinesAndUser();
                
                //ViewBag.Time=
                model.Line = lines;
                model.User = users.ToArray();
                ViewBag.phonenum = user.PhoneNum;
                ViewBag.gid = guide.Id;
                return View(model);
            }
            
        }

        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult personinformation()
        {
            long userid=(long)UserHelper.GetUserId(HttpContext);
            var user=UserSvc.GetById(userid);

            personViewModel model=new personViewModel();
            model.account = user.Account;
            model.phonenum = user.PhoneNum;
            model.nickname = user.NickName;
            model.gender = user.Gender;
            model.email = user.Email;

           long guideid= GuideUserSvc.GetGid(userid);
            var guide = GuideSvc.GetById(guideid);
            if (guide != null)
            {
                model.allschool = schoolSvc.GetAll();
                model.school = guide.school;
                model.intro = guide.intro;
            }

            ViewBag.guideid = guide;
            ViewBag.phonenum = user.PhoneNum;
           return View(model);
        }
        /// <summary>
        /// 个人资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPersonal(EditPersonalModel model)
        {
            var userid = UserHelper.GetUserId(HttpContext);
            long gid = GuideUserSvc.GetGid((long)userid);
            bool isok = UserSvc.UpdateUser((long)userid,model.nickname, model.phonenum, model.gender, model.password, model.email);
            if (gid == null)
            {            
                if (isok)
                {
                    return Json(new AjaxResult { status = "ok"});
                }
                return Json(new AjaxResult { status = "error", errorMsg = "保存失败" });
            }
           
            bool issuccess= GuideSvc.UpdateGuide(gid, model.school, model.intro);
            if (issuccess&&isok)
            {
                return Json(new AjaxResult { status = "ok" });
            }
                return Json(new AjaxResult { status = "error", errorMsg = "保存失败" });           
        }

        public ActionResult MyTravels()
        {
            var userid = Session["LoginUserId"];
            if (userid!=null)
            {
               var travel= TravelSvc.GetAll().Where(e => e.shenhe == true).ToArray();
                return View(travel);
            }
            return View();
        }

    }
}