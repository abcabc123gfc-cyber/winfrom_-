using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LINQ初识
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { 12, 123, 12, 3213, 12, 3213, 12, 312, 123, 12, 312, 3, 214, 45, 4, };

            //linq 出现方式有两种
            //1. 语句查询 微软官方推荐
            //2. 方法查询

            // 但是有些查询只能方法查询, 一般语句查询与方法查询一起使用

            //From 子句 用于给表达指定资源和循环变量
            //格式: from <变量> in <资源> select <表达式>
            //from 格式类似于 foreach item in collection
            //所有可以循环的资源, 都间接或直接继承自 IEnumerable 接口  IEnumerable<T>

            //linq 是 IEnumerable 或者 IEnumerable<T> 扩展的功能 也就是说所有可以被循环的数据, 都可以使用linq

            //原样返回 等同于 IEnumerable<int> ints.Select(x => x)
            IEnumerable<int> a = from x in ints select x;

            List<string> strings = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "103" };

            IEnumerable<char> chars = from x in strings from item in x select item;

            foreach (var item in chars)
            {
                Console.WriteLine(item);
            }

        }
    }
}
