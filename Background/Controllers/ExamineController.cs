using Common;
using DAL.IService;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace Background.Controllers
{
    public class ExamineController : Controller
    {
        public IGuideService GuideSvc { get; set; }
        public IGuideUserService GuideUserSvc { get; set; }
        public IUserService UserSvc { get; set; }
        public ITravelService TravelSvc { get; set; }

        // GET: Examine
        public ActionResult GuideList()
        {
            var guides = GuideSvc.GetByshenhe();
            List<UserDTO> list = new List<UserDTO>();
            foreach (var item in guides)
            {
                var userid = item.Users_Id;
                var user = UserSvc.GetById(userid);
                list.Add(user);
            }
            GuideListViewModel model = new GuideListViewModel();
            model.guide = guides;
            model.users = list.ToArray();
            return View(model);
        }


        public ActionResult Delete(long id)
        {
            GuideSvc.Shenhe(id);
            //GuideUserSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

        public ActionResult BatchDelete(long[] selectedIds)
        {
            foreach (var id in selectedIds)
            {
                GuideSvc.MarkDeleted(id);
                //GuideUserSvc.MarkDeleted(id);
            }
            return Json(new AjaxResult { status = "ok" });
        }


        public ActionResult Travels()
        {
            var travel = TravelSvc.GetByShenhe();
            return View(travel);
        }

        public ActionResult TravelDelete(long id)
        {
            TravelSvc.Shenhe(id);
            //GuideUserSvc.MarkDeleted(id);
            return Json(new AjaxResult { status = "ok" });
        }

    }
}