using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day05
{
    internal class 引用传参_值传参
    {
        public void Test(ref int a)
        {
            // 方法的参数传递分为两种: 值传递 引用传递
            // 值参数: 值参数传递的是参数的值, 值参数在方法调用结束后, 会被销毁
            //特殊情况
            //值类型 传递的是引用类型的参数,传递的是引用类型的指向堆内存地址的副本,在方法内创建的副本会指向同一块堆内存地址,修改数据会影响到方法外的引用类型

            // 引用参数: 使用 ref out in 关键字修饰的参数
            //in 修饰的参数，在方法内部严禁被修改
            // 引用参数传递的是参数的地址, 引用参数在方法调用结束后, 不会被销毁
            
        }
        public void PassValue(int a)
        {
            
            a = 100;
        }
        public void PassReferenceRef(ref int a)
        {
            //在调用方法之前需要先初始化 被ref 修饰的参数
            a = 100;
        }
        public void PassReferenceOut(out int a)
        {
            // 结束方法之前需要对 out修饰的参数 赋值
            a = 100;
        }
        public void PassReferenceIn(in int a)
        {
            // 使用 in 修饰的参数，在方法内部严禁被修改
            Console.WriteLine(a);
        }
    }
}
