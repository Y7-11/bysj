using CaptchaGen;
using Common;
using DAL;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Front.Controllers
{
    public class RegisterController : Controller
    {
        //给这个RegisterController定义了一个私有并且静态的属性，emmm也可能叫字段，应该是属性，用于后台验证验证码是否正确。
        private static string VerifyCode { get; set; }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        //这个方法是跟Main中的创建验证码是一样的，但是我认为register下面的方法就写在这个下面，或者提到公共方法里面给全部程序调用。
        public ActionResult CreateVerifyCode()
        {
            string verifycode = commonhelper.CreateVerifyCode(4);
            //每调用一次这个方法都会更新一次这个controller里面的VerifyCode属性。
            VerifyCode = verifycode;
            MemoryStream ms = ImageFactory.GenerateImage(verifycode, 50, 70, 20, 0);
            return File(ms, "image/jpeg");
        }

        public ActionResult Add(string obj, string verifyCode, string email)
        {
            //这里是利用正则对邮箱的格式进行验证
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            if (email != "" && !r.IsMatch(email))
            {
                return Content(JsonConvert.SerializeObject(new ContentResult { Content = "邮箱格式不正确！！" }));
            }
            //验证验证码是否正确，我没有参考登陆界面的验证方法，所以这个方法是比较独立的，如果后期出现问题要维护时，需要注意。
            if (VerifyCode != verifyCode)
            {
                return Content(JsonConvert.SerializeObject(new ContentResult { Content = "验证码错误！" }));
            }
            //将前端传来的json反序列化成一个指定的对象
            var model = JsonConvert.DeserializeObject<UserDTO>(obj);
            //调用DAL层面的方法，与数据库进行交互，存储数据
            UserService dal = new UserService();
            long result = dal.AddUser(model);
            long[] role = new long[1];
            role[0] = 7;
            new UserRoleService().AddUserRole(result, role);
            //根据返回的result进行分类判断
            if (result > 0)
            {
                return Content(JsonConvert.SerializeObject(new ContentResult { Content = "注册成功" }));//构造一个对面返回到前端，也就是ajax中返回的data
            }
            else if (result == -123)
            {
                return Content(JsonConvert.SerializeObject(new ContentResult { Content = "用户名已经存在！" }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new ContentResult { Content = "注册失败" }));
            }
        }
    }
}