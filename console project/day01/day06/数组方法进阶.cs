using System;
using System.Runtime.InteropServices;

namespace day06
{
    internal class 数组方法进阶
    {
        public void ArrayMethod()
        {
            //声明一个数组
            int[] arr = { 1, 2, 3, 4, 5 };

            //Find方法 自定义查询逻辑
            // 1. 目标函数  2. 函数方法
            //循环数组,传入方法中 ,接受 满足条件的bool 值,都不满足返回类型的默认值

            Console.WriteLine(Array.Find(arr, (x) => x < 2));
            //arr.Find(1);
            //FindIndex() 方法 查找索引,从前向后查找第一个满足条件的索引
            Console.WriteLine(Array.FindIndex(arr, (x) => x > 2));
            //FindLastIndex() 方法 从后向前查找第一个满足条件的索引
            Console.WriteLine(Array.FindLastIndex(arr, (x) => x > 2));
            //FindAll() 方法 查找 所有满足条件的元素
            int[] temp = Array.FindAll(arr, (x) => x > 2);
            Console.WriteLine();

            // TrueForAll() 方法 判断所有元素是否满足条件
            Console.WriteLine(Array.TrueForAll(arr, (x) => x < 1));

            //Exsts() 方法 判断所有元素是否 存在 满足条件的元素,存在返回true,否则返回false
            //注: 存在一个满足条件的元素,返回true,否则返回false
            Console.WriteLine(Array.Exists(arr, y => y < 1));
        }

        //创建一个自定义方法
        public void ArrayCustomize(int[] arr)
        {

        }
    }
    //声明一个数组的扩展方法
    public static class ArrayExtension
    {
        /// <summary>
        /// 自定义数组的Find方法
        /// </summary>
        /// <param name="arr">源数组</param>
        /// <param name="value">查找的元素</param>
        /// <returns>返回value 表示查找到,返回-1表示未查找到</returns>
        public static int Find(this int[] arr, int value)
        {
            foreach (var item in arr)
            {
                if (item == value)
                {
                    return item;
                }
            }
            return -1;
        }

        /// <summary>
        /// 自定义数组的FindLast方法
        /// </summary>
        public static int FindLast(this int[] arr, Func<int, bool> predicate)
        {
            int length = arr.Length - 1;
            for (int i = length; i >= 0; i--)
            {
                if (predicate(arr[i]))
                {
                    return arr[i];
                }
            }

            return -1;
        }

        /// <summary>
        /// 自定义数组的FindIndex方法
        /// </summary>
        public static int FindIndex(this int[] arr, Func<int, bool> predicate)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (predicate(arr[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 自定义数组的FindLastIndex1方法
        /// </summary>
        public static int FindLastIndex1(this int[] arr, Func<int, bool> predicate)
        {
            int length = arr.Length - 1;
            for (int i = length; i >= 0; i--)
            {
                if (predicate(arr[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 自定义数组的FindAll方法
        /// </summary>
        public static int[] FindAll(this int[] arr, Func<int, bool> predicate)
        {
           int [] arr1 = new int[arr.Length];
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (predicate(arr[i]))
                { 
                    arr1[count] = arr[i];
                    count++;
                }
            }
            return arr1;
        }

        /// <summary>
        /// 自定义数组的TrueForAll方法
        /// </summary>
        public static bool TrueForAll(this int[] arr, Func<int, bool> predicate)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (!predicate(arr[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 自定义数组的Exists方法
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Exists(this int[] arr, Func<int, bool> predicate)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (predicate(arr[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
