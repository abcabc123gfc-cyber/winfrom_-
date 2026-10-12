using System;
// 引用命名空间 泛型结构 接口 基类

namespace 泛型
{
    internal class 泛型_例子01
    {
        public static void Main(string[] args)
        {
            //泛型 实现了 可以迭代的 IEnumerable 接口
            //List<int> list = new List<int>();
            //for (int i = 0; i < 100; i++)
            //{
            //    list.Add(i);
            //}
            int[] a1 = { 1, 2, 3, 4, 5 };
            int[] a2 = { 1, 2, 3, 4, 5, 6 };
            double[] a3 ={
                1.1,2.2,3.3,4.4,5.5 };

            double[] a4 = { 11.1, 2.2, 3.3, 4.4, 5.5, 6.6 };

            var result = Zip(a1, a2);
            var resultDou = Zip(a3, a3);
            Console.WriteLine(string.Join(",", result));
            Console.WriteLine(string.Join(",", resultDou));
        }

        private static T[] Zip<T>(T[] a, T[] b)
        {
            T[] zippend = new T[a.Length + b.Length];
            int ai = 0, bi = 0, zi = 0;
            do
            {
                if (ai < a.Length)
                {
                    zippend[zi++] = a[ai++];

                }
                if (bi < b.Length)
                {
                    zippend[zi++] = b[bi++];
                }

            } while (ai < a.Length || bi < b.Length);
            return zippend;
        }
    }
}
