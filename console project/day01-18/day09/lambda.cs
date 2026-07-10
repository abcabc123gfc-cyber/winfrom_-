using System;

namespace day09
{
    internal class lambda
    {
        // labbda 表达式 本质上也是一个函数，他只是匿名函数
        // 在C#编程中，他是强类型编程语言，他必须指定参数的类型，返回值类型
        // 方法的类型就是委托类型  原因:委托是一种基于方法的类型
        // 将方法作为变量存储起来，使用委托
        // 委托分为两大类： 1.有返回值 2.无返回值

        // 有返回值格式：Func<参数列表,返回值类型> 变量名；
        // 无返回值格式：Action<参数列表> 变量名；

        // 使用 Delegate 
        //有返回值 格式: delegate 返回值类型 函数名(参数列表);
        //无返回值格式: delegate void 函数名(参数列表);


        #region lambda 表达式简化过程

        //第一种情况： 参数类型可以省略不写
        Func<int, int, int> func = (a, x0) => a * a;

        //第二种情况： 只有一个参数时，参数括号可以省略
        Action<int> func1 = x => Console.WriteLine();

        //第三种情况： 如果方法体只有一行代码，{} 可以省略,return 可以省略
        Func<int, int> func2 = a => a * a;
        #endregion

        #region Delegate 自定义委托类型
        delegate int fu1n(int a, int b);
        delegate void fun2();
        public int Test(int a, int c)
        {
            // 使用Delegate 自定义委托类型，无返回值
            fun2 fun = Test1;
            return default;
        }
        public void Test1()
        {
            // 使用Delegate 自定义委托类型，有返回值
            fu1n fun = Test;
        foreach (var item in new int[] { 1, 2, 3, 4, 5 })
            {
                Console.WriteLine(item);
            }
        }

        #endregion

    }
}
