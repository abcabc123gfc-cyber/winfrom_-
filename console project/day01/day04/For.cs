using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class For
    {
        public void Test()
        {
            //fro循环
            //循环四要素:循环变量,循环条件,循环变量的更新,循环体
            //语法格式:for(初始化;条件;更新){循环体}
            //作用:循环执行代码
            //执行顺序:
            //初始化->条件->循环体->更新->条件->循环体->更新->条件->循环体->更新->条件->循环...
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            //死循环
            //第一种
            /* for (; ; )
             {
                 Console.WriteLine("死循环");
             }*/
            //第二种
            /*for (; true;)
            {
                true 等价于 条件表达式为true
                Console.WriteLine("死循环");
            }*/
            //练习
            {
                //循环5次,取平均
                /*int sum = 0;
                for (int i = 0; i < 5; i++)
                {

                    sum += int.Parse(Console.ReadLine());

                }
                Console.WriteLine(sum / 5.0);*/
            }
        }
    }
}
