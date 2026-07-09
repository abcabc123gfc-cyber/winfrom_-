using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day15
{
    internal class 密封类
    {
        //使用 sealed 关键字修饰的类 为 密封类
        //使用 static 关键字修饰的类 为 静态类
        
        //实例化: 普通类 静态类 可以实例化, 抽象类不能实例化
        //密封类与普通类的区别: 不能被继承

    }
    /// <summary>
    /// 密封类
    /// </summary>
    sealed class People:密封类
    {

    }
    /// <summary>
    /// 静态类
    /// </summary>
    static class People1
    {

    }

}
