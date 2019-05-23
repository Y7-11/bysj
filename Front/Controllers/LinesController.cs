using Common;
using DAL.IService;
using Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Front.Controllers
{
    [Filter]
    public class LinesController : Controller
    {
        public ILineService LineSvc { get; set; }

        public IOrderService OrderSvc { get; set; }

        public IGuideUserService GuideUserSvc { get; set; }
        public IGuideService GuideSvc { get; set; }
        public IGuidLinesService GuideLineSvc { get; set; }
        public IOrderUserService OrderUserSvc { get; set; }
        public IUserService UserSvc { get; set; }
        public ILineInfoService LineInfoSvc { get; set; }
        // GET: Lines
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            int count = 0;
            int totalpage = 0;
            var guide = LineSvc.Page(page, out count, out totalpage);
            ViewData["totalpage"] = totalpage;
            ViewBag.phonenum = user.PhoneNum;
            return View(guide);
        }

        [HttpPost]
        public ActionResult Index(string search, int page = 1)
        {
            int count = 0;
            int totalpage = 0;
            var guide = LineSvc.GetTitle(search, page, out count, out totalpage);
            ViewData["totalpage"] = totalpage;
            return View(guide);
        }


        public ActionResult Info(long id)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            var lines = LineSvc.GetByLid(id);
            var linesinfo = LineInfoSvc.GetById(id);
            LInfo model = new LInfo();
            model.Lines = lines;
            model.LineInfo = linesinfo;
            ViewBag.phonenum = user.PhoneNum;
            return View(model);
        }

        public int checkyuyue(long lid)
        {
            long userid = (long)Session["LoginUserId"];
            long guideid = GuideLineSvc.GetGid(lid);
            var guide = GuideSvc.GetById(guideid);
            if (guide != null)
            {
                if (guide.Users_Id == userid)
                {
                    return 1;
                }

                if (guide.isappointment)
                {
                    return 2;
                }
            }
            else
            {
                return 4;
            }

            long[] oid = OrderUserSvc.GetById(userid);
            var tids = OrderSvc.GetByTids(oid);
            foreach (var item in tids)
            {
                if (lid == item)
                {
                    return 3;
                }
            }
            return 0;
        }

        [NoFilter]
        public ActionResult Btn_Yuyue(long? id)
        {
            long userid = (long)Session["LoginUserId"];
            int ischeck = checkyuyue((long)id);
            if (ischeck == 1)
            {
                return Json(new AjaxResult { status = "error", errorMsg = "无法预约自己" });
            }
            if (ischeck == 2)
            {
                return Json(new AjaxResult { status = "error", errorMsg = "导游未开启预约" });
            }
            if (ischeck == 3)
            {
                return Json(new AjaxResult { status = "error", errorMsg = "已经预约了" });
            }
            if (ischeck == 4)
            {
                return Json(new AjaxResult { status = "error", errorMsg = "导游不存在" });
            }
            long oid = OrderSvc.CreateOrder((long)id);
            if (oid != null)
            {
                using (TransactionScope scope = new TransactionScope())//事物
                {

                    var issuccess = OrderUserSvc.appointment(userid, oid);
                    if (issuccess)
                    {
                        bool isadd = LineSvc.AddNumOfPeople((long)id);
                        if (!isadd)
                        {
                            return Json(new AjaxResult { status = "error", data = "预约失败" });
                        }
                        scope.Complete();
                        return Json(new AjaxResult { status = "success", data = "预约成功" });
                    }
                    else
                    {
                        return Json(new AjaxResult { status = "error", errorMsg = "预约失败" });
                    }
                }
            }
            return Json(new AjaxResult { status = "error", errorMsg = "" });
        }




        /// <summary>
        /// 添加路线
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult AddLines(long gid)
        {
            ViewBag.gid = gid;
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            ViewBag.phonenum = user.PhoneNum;
            //ViewBag.lid = GuideLineSvc.GetLid(gid);
            return View();
        }


        [HttpPost]
        public ActionResult AddLines(long gid, LineAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            long lid = LineSvc.AddLine(model.Province, model.city,
                model.intro, model.title, model.PastPrice, model.discount);

            ViewBag.lid = lid;

            long GLid = GuideLineSvc.AddGidLid(gid, lid);

            return Json(new AjaxResult { status = "ok" ,data=lid });

        }

        [HttpGet]
        public ActionResult InfoList(long lid)
        {
            var infos = LineInfoSvc.GetById(lid);
            ViewBag.lid = lid;
            return View(infos);
        }

        [HttpPost]
        public ActionResult InfoList(long lid,AddLinesInfo linesInfo)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }

            long id = LineInfoSvc.AddLineInfo(lid, linesInfo.Address, linesInfo.ScenicSpot, 
                linesInfo.SpotInfo,linesInfo.Apartment , linesInfo.Level, linesInfo.Facilities, linesInfo.attention, linesInfo.tag);

            ViewBag.lid = lid;

            return Json(new AjaxResult { status = "ok" });
        }
    }
}