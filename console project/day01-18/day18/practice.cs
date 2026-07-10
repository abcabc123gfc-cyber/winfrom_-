using System;
using System.Collections.Generic;

namespace day18
{
    public class Practice
    {

        delegate void ccfMathOperation(int a, int b);
        static ccfMathOperation add = new ccfMathOperation(Add);
        public static void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        public static void Multiply(int a, int b)
        {
            Console.WriteLine(a * b);
        }
        public Practice()
        {
            add += Multiply;

        }
        public static void Test(int a, int b)
        {
            add(a, b);
        }

        /// <summary>
        /// 泛型方法_从后向前查找满足条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FindLast<T>(T[] arr, Func<T, bool> predicate)
        {
            foreach (var item in arr)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default;
        }

        public static T[] FindAll<T>(T[] arr, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();
            foreach (var item in arr)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        public static T FindIndex<T>(T[] arr, Func<T, bool> predicate)
        {
            foreach (var item in arr)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return (T)(object)-1;
        }

        public static bool TrueForAll<T>(T[] arr, Func<T, bool> predicate)
        {
            foreach (var item in arr)
            {
                if (predicate(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
