using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day06
{
    internal class 数组
    {
        public void ArrayDemo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("hello");
            sb.Remove(0, 1);
            // 替换
            sb.Replace("l", "L");
            sb.Insert(0, "h");
            //sb.Clear(); 
            int temp = sb.Length;

            //数组:存储同种数据类型的容器,索引从0开始,到数组长度-1

            //一维数组
            //声明
            {
                //变量名 :通常以复数的形式命名
                //存储长度为5的整型数组
                int[] arr = new int[5];
                int[] arr1 = new int[5] { 1, 2, 3, 4, 5 };
                int[] arr2 = { 1, 2, 3, 4, 5 };
                //People[] peoples = new People[5];
                //数组初始化会使用当前类型默认值进行占位,
                //int : 0,char : '\0',bool : false

            }
            #region 数组操作_练习

            {
                //    int[] arr = new int[50];
                //    arr[0] = 10;
                //    int temp1 = 0;
                //    for (int i = 1; i <= 100; i++)
                //    {

                //        if (i % 2 == 0 && temp1 < 50)
                //        {
                //            arr[temp1] = i;
                //            temp1++;
                //        }

                //    }
                /*foreach (int i in arr)
                {
                    Console.WriteLine(i + " " + GetHashCode().ToString());
                    
                }*/

                //练习2 
                //声明一个长度10且初始化了的数组
                int[] arr2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                // 自定义排序 从大到小:
                Array.Sort(arr2, (x, y) => x < y ? 1 : -1);
                //插入排序 从大到小
                //for (int i = 1; i < arr2.Length; i++)
                //{
                //    temp = arr2[i];
                //    int j = i - 1;
                //    for (; j >= 0; j--)
                //    {
                //        if (arr2[j] < temp)
                //        {
                //            break;
                //        }
                //        arr2[j + 1] = arr2[j];
                //    }
                //    arr2[j + 1] = temp;
                //}
                foreach (int i in arr2)
                {
                    Console.WriteLine(i);
                }

            }
            #endregion

            #region foreach
            //格式 foreach(数据类型 变量名 in 数组名)
            //{
            //    //变量名 : 数组中的元素
            //}


            #endregion
        }

        public void Sotr_RandomtDemo()
        {
            //冒泡排序
            //int[] arr = { 5, 4, 3, 2, 1 };
            //for (int i = 0; i < arr.Length - 1; i++)
            //{
            //    for (int j = 0; j < arr.Length - 1 - i; j++)
            //    {
            //        if (arr[j] > arr[j + 1])
            //        {
            //            (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
            //        }
            //    }
            //}
            #region 随机数


            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(random.Next(0, 10));
                for (int j = 0; j < i; j++)
                {
                    if (list[j] == list[i])
                    {
                        list.RemoveAt(i);
                        i--;
                        break;
                    }
                }
                Console.WriteLine(list[i]);
            }
            #endregion
        }
    }
   
}
