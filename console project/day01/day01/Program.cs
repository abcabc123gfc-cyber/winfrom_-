//命名空间 :可以看做一个范围，里面存放着变量、方法、类、接口、枚举等等,标注一些成员的作用域
//命名空间可以有多个
//使用using 引入命名空间 
//namespace 声明命名空间
//命名空间在一个范围 名称不允许重复
using System;

namespace day01
{
    // class 声明类
    //internal 修饰符
    //Main() : 入口方法, 程序从这里开始执行
    internal class Program
    {
        static void Main(string[] args)
        {
            //单行注释 用来描述代码块 快捷键 Ctrl + /
            /*
             * 多行注释 快捷键 Ctrl + Shift + /
             * string [] args : 接数命令行传入的参数 可以存储字符串数组
             * 
             */

            /// 
            /// <summary>
            /// 文档注释 主要用在对类和方法上面,对某一块代码整体的描述
            /// </summary>
            #region 折叠代码块
        
            #endregion
            控制台常用方法 console = new 控制台常用方法();
            console.function();
            //console.function2();
            //console.function3();
            //console.ProjectProperties();
            console.DerivedValue();





        }
    }
}
