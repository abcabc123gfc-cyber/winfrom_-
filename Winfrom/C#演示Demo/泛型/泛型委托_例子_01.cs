using System;

namespace 泛型
{
    internal class 泛型委托_例子_01
    {
        public static void Main(string[] args)
        {
            Action<string> a = new Action<string>(Say);
            a.Invoke("hello world");
            Func<int, int> func = new Func<int, int>(Mul);
            //拉姆达表达式 声明函数
            func += (int index)=>index * 100;
            Console.WriteLine(func.Invoke(10));
        }
        static void Say(string s)
        {
            Console.WriteLine(s);
        }
        static int Mul(int x)
        {
            return (x * 100);
        }
    }
}
