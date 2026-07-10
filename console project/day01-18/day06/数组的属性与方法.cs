using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day06
{
    internal class 数组的属性与方法
    {
        public void ArrayFunction()
        {
            // 数组都是Array 类的实例 Array提供了一系列的方法和属性
            int[] arr = { 1, 2, 3, 4, 5 };
            int[,] arr2 = { { 1,2,3,4,5 }, { 2,3,4,5,6 } };

            //Length 属性可以获取到数组的总长度 32位
            //LongLength 属性可以获取到数组的总长度 64位\
            Console.WriteLine(arr.Length+"_ 数组的长度Length");
            Console.WriteLine(arr2.LongLength+"_ 数组的长度 LongLength");

            //Rank 属性可以获取到数组的维数
            Console.WriteLine(arr.Rank);
            Console.WriteLine(arr2.Rank);

            //1  Clear() 方法 可以清空数组
            // 参数1 数组 2.恢复默认值的 起始索引 3.长度
            Array.Clear(arr, 0, arr.Length);

            //2 Copy() 方法 可以复制数组
            //参数1 源数组 2 目标数组 3.复制的长度
            Array.Copy(arr, arr2, arr.Length);
            // 1. 源数组 2.源数组的索引 3.目标数组 4.目标数组的索引 5.复制的长度
            Array.Copy(arr, 0, arr2, 0, arr.Length);    

            //3 Resize() 方法 可以改变数组的长度
            Array.Resize<int>(ref arr, 30);
            
        }
    }
}
