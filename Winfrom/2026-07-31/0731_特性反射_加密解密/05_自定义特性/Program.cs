using _05_自定义特性.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _05_自定义特性
{
    //[MyAttribute("1.0","类","2026-07-31")]
    [My("1.0","类","2026-07-31")]
    internal class Program
    {
        //[My("1.0", "属性", "2026-07-31")]
        public int Id { get; set; }
        static void Main(string[] args)
        {

            //获取 类 方法 属性 的 通过特性添加的元数据 
            //如何获取?
            //通过反射

            //原始调用方法的方式:
            //通过类名或者实例名.方法名()
            // Program.Test();
            //通过反射调用方法
            //获取Program类的类型 因为Test在Program类中
            Type t1 =typeof(Program);
            //从Program的类型中获取Test1方法
            MethodInfo m1 = t1.GetMethod("Test");

            m1.Invoke(null,null);//调用方法

          object [] attrs=  m1.GetCustomAttributes(typeof(MyAttribute),false);

            foreach (MyAttribute attr in attrs)
            {
                Console.WriteLine(attr.Version);
                Console.WriteLine(attr.Message);
            }

        }

        [My("1.0", "方法", "2026-07-31")]
      //  [My("1.0", "方法", "2026-07-31")]
        [My2("2.0", "方法3123", "2026-07-31")]
        public static void Test()
        {
            Console.WriteLine("Test");
        }
    }
}
