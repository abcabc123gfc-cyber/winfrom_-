using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day11
{
    internal class 静态成员
    {
        //非静态成员可以访问静态成员
        //静态成员不能访问非静态成员

        //区别:
        //1. 访问方式不同: 静态成员通过: 类名.成员名 非静态成员通过: 对象.成员名
        //2  存储位置不同: 静态成员存储在类上,只有一份 所有实例共享一个静态成员
        //非静态成员存储在实例上,每个实例有自己一份

        

        
        //非静态成员
        public int a;
        //非静态方法
        public void Demo()
        {
            Demo2();
            Console.WriteLine("非静态方法");
        }
        //静态成员
        public static int b;
        //静态方法
        public static void Demo2()
        {
            Console.WriteLine("静态方法");
        }

    }
}
