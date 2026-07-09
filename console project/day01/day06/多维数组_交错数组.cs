using System;

namespace day06
{
    internal class 多维数组_交错数组
    {
        public void Arraymore()
        {
            //多维数组
            //一个二维数组的定义可以理解为 :一个有行与列的表格

            //多维数组的声明
            {
                //1.声明 格式：数据类型[行][列] 数组名=new 数据类型[行][列]
                int[,] arr = new int[3, 4];
                int[,] arr1 =
                {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 }
                };


            }
            //三维数组,思维数组
            {
                int[,,] arr = new int[2, 3, 4];
                int[,,] arr1 = {
                     {
                        { 1, 2, 3, 4 },
                        { 5, 6, 7, 8 },
                        { 9, 10, 11, 12 }
                    },
                    {
                        { 13, 14, 15, 16 },
                        { 17, 18, 19, 20 },
                        { 21, 22, 23, 24 }
                    }};
                //foreach (int i in arr1)
                //{

                //    Console.WriteLine(i);
                //}
                //是哟拍给你for遍历
                for (int i = 0; i < arr1.GetLength(0); i++)
                {

                    for (int j = 0; j < arr1.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr1.GetLength(2); k++)
                        {
                            Console.WriteLine(arr1[i, j, k]);
                        }
                    }

                }
            }

        }

        public void JiaoCuoArray()
        {
            //交错数组 : 数组中存放数组

            //交错数组 声明格式:
            //一维数组 声明格式: 数据类型[] 数组名=new 数据类型[长度]
            int[] arr = new int[5];
            //二维数组 声明格式:
            //数据类型[][] 数组名=new 数据类型[数组个数][]

            //三维数组 声明格式:
            // 数据类型[][][] 数组名=new 数据类型[二维数组个数][一维数组个数][]
            int[][] arr1 = new int[3][];
            int [][] arr2 =
            {
                new int[3],
                new int[5],
                new int[7]
            };
           
            //arr.GetEnumerator();
            //获取值: 一维数组 数组名[索引]; 设置 值: 一维数组 索引=值;
            //二维交错数组数组 数组名[索引][索引]; 设置 值: 二维数组 索引[索引]=值;
            int temp= arr2[2][2];
            Console.WriteLine(temp);
            dynamic temp1 = arr2[2][2];


        }
    }
}
