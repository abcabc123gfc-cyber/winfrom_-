using System;

namespace 打印7
{
    public class 泛型约束
    {
        //泛型: 未知的类型, 
        //泛型约束:对泛型的类型做出限制

        //泛型接口 泛型类 泛型方法 都可以添加泛型约束

        public static void Fn1<T>(T t)
        {
            Console.WriteLine(t.GetType());
            if (t is int)
            {
                Console.WriteLine("int");
            }
            else
            {
                Console.WriteLine("other");
            }
        }
        //泛型约束: 泛型类中, 泛型参数T必须继承自class
        public static void Fn2<T>(T t) where T : class
        {
            Console.WriteLine(t.GetType());
        }
        //泛型约束: 泛型类中, 泛型参数T必须实现Icomparable接口
        public static void Fn3<T>(T t) where T : struct
        {
            Console.WriteLine(t.GetType());
        }
        //泛型约束: 泛型类中, 泛型参数T必须提供无参构造函数的;;类
        public static void Fn4<T>(T t) where T : new()
        {
            Console.WriteLine(t.GetType());
        }
        //泛型约束: 泛型类中, 泛型参数T必须继承自People类,对具体的类做出约束
        public static void Fn5<T>(T t) where T : People
        {

        }

    }
   public  class People
    {

    }
    //泛型约束: 泛型类中, 泛型参数T必须继承自People类,对抽象类做出约束
   abstract public class Student <T> where T : People
    {

    }
}
