using System;

namespace day07
{
    internal class 控制台同步代码
    {
       
        public static void Test()
        {
            //同步: 一个任务结束后, 才能开始下一个任务
            //异步: 任务之间可以同时进行
            Console.WriteLine("任务1");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("任务2");
        }
    }
}
