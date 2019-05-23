using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AjaxResult
    {
        /// <summary>
        /// 执行的结果
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// 执行返回的数据
        /// </summary>
        public object data { get; set; }
    }
}
