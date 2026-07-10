using System;

namespace day18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //泛型委托 func = new 泛型委托();
            ////初始化字符串数组
            //string[] arr = new string[5] { "罗111", "小", "小", "小", "小" };
            //Console.WriteLine(func.Fn2(arr, IsChar));
            Practice p = new Practice();
            Practice.Test(12, 13);

            int[] arr = new int[5] { 1, 2, 3, 4, 5 };

            Console.WriteLine(Practice.FindLast(arr, Last));
            int[] arr1 = Practice.FindAll(arr, All);
            int index = Practice.FindIndex(arr, Last);
            if (Practice.TrueForAll(arr, All))
            {
                Console.WriteLine("全部满足条件");
            }
        }
        static bool Last(int a)
        {
            if (a > 0)
            {
                return true;
            }
            return false;
        }
        public static bool All(int a)
        {
            if (a > 3)
            {
                return true;
            }
            return false;
        }
        static bool IsChar(string s)
        {
            if (s.StartsWith("罗"))
            {
                return true;
            }
            return false;
        }



    }
}
