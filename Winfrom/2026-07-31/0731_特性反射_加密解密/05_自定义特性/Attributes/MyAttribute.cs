using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_自定义特性.Attributes
{



    //特性本质上就是一个类
    //1.命名:以大驼峰命名,以Attribute结尾
    //2.必须继承Attribute特性

    //AttributeTargets 枚举  设置当前特性 可以添加到那个元素上  默认值时AttributeTargets.All 所有的元素都可以添加(类 属性 字段 结果 方法  事件  委托...都可以用
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method,AllowMultiple =false, Inherited=false)]
    public class MyAttribute:Attribute
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
        public MyAttribute(string v, string m, string c){

            Version = v; 
            Message = m; 
            CallTime = c;
        }
    }

    class My2Attribute : MyAttribute
    {
        public My2Attribute(string v, string m, string c) : base(v, m, c)
        {
        }
    }
}
