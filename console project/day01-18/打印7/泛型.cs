using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打印7
{
    internal class 泛型
    {
        //定义一个泛型方法
        //在方法名后面自动添加一个<> 里面相当于类型的形参列表
        //泛型的命名规范: 一般以 T 开头, 尽量做到能够表达这个类型的作用
        //如果使用一个单词作为泛型, 使用 T

        //默认情况下,在定义方法时, 需要指定该参数的类型,而通过泛型,就可以把"指定类型" 这个操作, 延迟到方法调用的时候

        public static void Print<T>(T t)
        {
            Console.WriteLine(t);
        }
        public static T Print<T>(T t, int count)
        {
            //返回默认值, 返回一个指定类型的默认值
            //比如: int 的默认值是 0
            return default;
        }
        #region 泛型的代码复用
        //public static void ShowInt(int a)
        //{
        //    Console.WriteLine($"类型的参数为{a.GetType()},类型的值为{a}");
        //}
        //public static void ShowString(string a)
        //{
        //    Console.WriteLine($"类型的参数为{a.GetType()},类型的值为{a}");
        //}

        //上述2个方法, 完全重复, 仅仅是参数的类型不同
        //泛型, 可以解决这个问题
        public static void Show<T>(T a)
        {
            Console.WriteLine($"类型的参数为{a.GetType()},类型的值为{a}");
        }

        //在没有泛型的时候,如果需要封装需要使用 object
        //优点: 能够实现代码复用
        //缺点: 类型转换为object类型,会进行装箱操作, 损失性能
        #endregion
        public static T Add<T>(T a, T b)
        {
            dynamic c = a + b;
            return c;
        }

        //总结
        //泛型就是未知的类型
        //泛型接口: 在实例化的时候需要指定类型 

    }

}
