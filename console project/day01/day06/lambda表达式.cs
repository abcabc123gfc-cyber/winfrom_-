using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day06
{
    internal class lambda表达式
    {
        public void TestLambda()
        {
            //Lambda 表达式: 就是另一种函数的写法
            //格式: (参数列表) => {方法体} ,匿名方法

            //Lambda表达式: 不是一个独立可调用的方法,必须赋值给一个方法
            //原因: 函数的调用,必须知道函数的声明
            //方法的类型 就是委托类型
            //委托类型分为: 有返回值 和无返回值
            //有返回值类型的委托声明格式: Func<参数列表,返回值类型> 变量名;
            Func<int, int> func = (int x) => x > 0?x:-1;

            Console.WriteLine( func(2));
        }
    }
}
