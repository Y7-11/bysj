using Common;
using DAL.IService;
using Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Front.Controllers
{
    [Filter]
    public class TravelsController : Controller
    {
        public ITravelService TravelSvc { get; set; }
        public IUserTravelsService UserTravelSvc { get; set; }
        public IUserService UserSvc { get; set; }
        // GET: Travels
        [HttpGet]
        public ActionResult Index(int page=1)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            int count = 0;
            int totalpage = 0;
            var travels = TravelSvc.Page(page, out count, out totalpage);
            ViewData["totalpage"] = totalpage;
            ViewBag.phonenum = user.PhoneNum;
            return View(travels);
        }

        [HttpPost]
        public ActionResult Index(string search, int page = 1)
        {
            int count = 0;
            int totalpage = 0;
            var guide = TravelSvc.GetTitle(search, page, out count, out totalpage);
            ViewData["totalpage"] = totalpage;
            return View(guide);
        }

        private bool isclick = false;
        public ActionResult Info(long id)
        {
            var user = UserSvc.GetById(UserHelper.GetUserId(HttpContext).Value);
            var Travels = TravelSvc.GetByLid(id);
            UserTravels model = new UserTravels();
            model.travels = Travels;
            model.user = user;
            isclick=UserTravelSvc.IsClick(user.Id, Travels.Id);
            ViewBag.isclick = isclick;
            ViewBag.phonenum = user.PhoneNum;
            return View(model);
         
        }
        public ActionResult click_tops(long id)
        {
                //var userid=UserHelper.GetUserId(HttpContext);
                //if (userid==null)
                //{
                //    return Json(new AjaxResult { status = "error", data = "请先登陆" });
                //}
            long uid = (long)UserHelper.GetUserId(HttpContext);
            var count = TravelSvc.Updatetops(id);
            if (count==null)
            {
                return Json(new AjaxResult { status = "error" });
            }
            UserTravelSvc.ClickTops(uid, id);
            return Json(new AjaxResult { status = "ok",data= count });
        }

        [HttpGet]
        public ActionResult AddTravels()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddTravels(AddTravelsModel travels)
        {
            if (!ModelState.IsValid)
            {
                string msg = mvchelper.GetValiMsg(ModelState);
                return Json(new AjaxResult { status = "error", errorMsg = msg });
            }
            var uid= (long)UserHelper.GetUserId(HttpContext);
            var id = TravelSvc.AddTravel(travels.title, travels.content, travels.tops, uid);


            return Json(new AjaxResult { status = "ok" });

        }

        public ActionResult BatchDeleteTravel(long[] dels)
        {
            foreach (long id in dels)
            {
                TravelSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }
    }
}