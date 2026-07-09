using System;

namespace day04
{
    internal class NestedLoop
    {
        // continue : 跳过当前循环中的一次, 继续下一次循环
        // break : 结束当前循环
        //return : 结束方法


        //循环嵌套
        //把内层循环当做外层循环的循环体, 外层执行一次, 内层循环执行全部
        public void TestFor()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine("i = {0}, j = {1}", i, j);
                }
            }
        }
        public void TestWhile()
        {

            //while循环
            //格式:while(条件){循环体}
            /*int i = 0;
            while (i < 5)
            {
                Console.WriteLine("i = {0}", i);
                i++;
            }*/


            //do...while循环
            //格式:do{循环体}while(条件)
            /*i = 0;
            do
            {
                Console.WriteLine("i = {0}", i);
            }while (i < 5);*/


            //死循环
            /*while (true)
            {
                Console.WriteLine("死循环");
            }*/


        }
    }
}
