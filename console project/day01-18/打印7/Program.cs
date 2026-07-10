using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打印7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //<> 决定泛型类型
            Person<string> p1 = new Person<string>();
            p1.language = "1233";

            泛型约束 fan=new 泛型约束();
            fan.Fn1<int>(1);
        }
    }
}
