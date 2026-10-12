using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 反射
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
            //反射不是C#的特性，而是.NET的，C#只是提供了反射的语法 
            //reflection 反射 在镜子中看到的虚像 

            // 通过一个对象 在不使用 new操作符 与 不知道这个对象是什么静态类型的情况下,创建出一个同类型对象
            //可以创建对象，获取对象属性，调用对象方法，获取对象类型信息
            //进一步解耦，
            //单元测试 依赖出入 泛型编程 基于反射的动态加载

            //为什么需要反射
            //程序的逻辑不是在 写程序的时候确定, 有时需要用户与程序进行交互的时候才可以确认逻辑
            //此时, 程序处于运行状态, 此时, 运行状态的代码无法确定,
            //如果 在程序处于编写状态去 预测 与 枚举 用户可能做出的操作,程序会变得非常臃肿
            //如: 大量的 if else 语句, 也无法完全覆盖所有可能操作
            //此时, 需要使用反射
        //}
        //反射原理与依赖注入
    }
}
