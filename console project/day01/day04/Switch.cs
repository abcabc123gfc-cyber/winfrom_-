using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day04
{
    internal class Switch
    {
        public void Test()
        {
            //Switch 语句 分支结构中分支结构的一种
            //执行逻辑:等值判断,匹配成功则执行对应逻辑代码
            //传递进来的值和case 后面的值类型必须一致
            //可嵌套,case可并列
            //语法结构
            /*switch (表达式)
            {
                case 表达式1:
                    逻辑代码
                    break;
                case 表达式2:
                    逻辑代码
                    break;
                case 表达式3:
                case 表达式4:
                    逻辑代码
                    break;
                default:
                    逻辑代码
                    break;
            }*/
            //int a =int.Parse( Console.ReadLine());
            int a = 1;
            switch (a)
            {
                case 1:
                    Console.WriteLine("星期一");
                    break;
                case 2:
                    Console.WriteLine("星期二");
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }

        }
    }
}
