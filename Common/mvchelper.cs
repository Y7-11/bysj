using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
{
    public class mvchelper
    {
        public static string GetValiMsg(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count <= 0)
                {
                    continue;
                }
                sb.Append("属性【").Append(key).Append("】错误：");
                foreach (var modelError in modelState[key].Errors)
                {
                    sb.Append(modelError.ErrorMessage);
                }
            }
            return sb.ToString();
        }

        public static string ToQueryString(NameValueCollection nvc)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in nvc.AllKeys)
            {
                string value = nvc[key];
                sb.Append(key).Append("=").Append(Uri.EscapeDataString(value)).Append("&");
            }
            return sb.ToString().Trim('&');
        }
        public static string RemoveQueryString(NameValueCollection nvc, string name)
        {
            NameValueCollection newNvc = new NameValueCollection(nvc);
            newNvc.Remove(name);
            return ToQueryString(newNvc);
        }
        public static string UpdateQueryString(NameValueCollection nvc, string name, string value)
        {
            NameValueCollection newNVC = new NameValueCollection(nvc);
            if (newNVC.AllKeys.Contains(name))
            {
                newNVC[name] = value;
            }
            else
            {
                newNVC.Add(name, value);
            }
            return ToQueryString(newNVC);
        }
    }
}
