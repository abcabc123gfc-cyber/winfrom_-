using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day06.attributes
{
    //特性本质上就是一个类，继承自Attribute类
    //1. 命名: 以大驼峰命名, 以Attribute结尾
    //2. 必须继承Attribute特性
    internal class MyAttribute:Attribute
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public string CallTime { get; set; }
        public MyAttribute(string v, string m, string c)
        {

            Version = v;
            Message = m;
            CallTime = c;
        }


    }
}
