using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace day04
{
    internal class 练习
    {
        public void JudgeStrign()
        {
            /*   Console.WriteLine("输入字符,判断是字符串类型");
               string str = Console.ReadLine();
               if (str != null)
               {
                   Console.WriteLine("输入的字符是字符类型");
               }
               else
               {
                   Console.WriteLine("输入为空");
           } */



            //判断星期天 特价菜
            /* Console.WriteLine("请输入星期几：以判断特色菜类型");
             int week = Convert.ToInt32(Console.ReadLine());
             switch (week)
             {
                 case 1:
                     Console.WriteLine("星期一");
                     Console.WriteLine("牛脾");
                     break;
                 case 2:
                     Console.WriteLine("星期二");
                     Console.WriteLine("啤酒鸭");
                     break;
                 case 3:
                     Console.WriteLine("星期三");
                     Console.WriteLine("牛排");
                     break;
                 case 4:
                     Console.WriteLine("星期四");
                     Console.WriteLine("牛腩");
                     break;
                 case 5:
                     Console.WriteLine("星期五");
                     break;
                 case 6:
                     Console.WriteLine("星期六");
                     break;
                 case 7:
                     Console.WriteLine("星期天");
                     break;
                 default:
                     Console.WriteLine("输入错误");
                     break;
             }*/

            //简易计算
            /*{
                Console.WriteLine("请输入数字：");
                //double a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("请输入运算符号：");
                //string op = Console.ReadLine();
                Console.WriteLine("请输入数字：");
                //double b = Convert.ToDouble(Console.ReadLine());
                //使用switch
                switch (op)
                {
                    case "+":
                        Console.WriteLine(a + b);
                        break;
                    case "-":
                        Console.WriteLine(a - b);
                        break;
                    case "*":
                        Console.WriteLine(a * b);
                        break;
                    case "/":
                        Console.WriteLine(a / b);
                        break;
                    default:
                        Console.WriteLine("输入错误");
                        break;
                }
            }*/

            //输出1-100 的数字 8个一行,3包括其倍数不输出
            Console.WriteLine("输出1-100 的数字 8个一行,3包括其倍数不输出");
           /* int j = 1;
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0) i++;
                if(i / 10 == 3)i++;
                if (i % 10 == 3)i++;
                
                Console.Write(i + "\t");
                j++;
                if (j % 8 == 0)
                {
                    Console.Write(++i + "\t");
                    Console.WriteLine();
                    j = 1;
                }
            }*/

            // 
            /* int j=0;
             int k=0;
             int i = 1;
             for (i=1; i <= 10; i++)
             {
                switch(i)
                {
                    case 1:
                         break;
                    case 2:
                         j+=i;

             //注: i 和k 顺序相反 
                         break;
                    case 3:
                        i+=j;
                         break;
                    case 4:
                        i+=k;
                         break;
                    case 5:
                        i+=j;
                         break;
                    case 6:
                        i+=k;
                         break;
                    case 7:
                        i+=j;
                         break;
                    case 8:
                        i+=k;
                         break;
                    case 9:
                        i+=k;
                         break;

                }
             }
             Console.WriteLine($"1-10素数{j}");
             Console.WriteLine("1-10合数{0}",k);*/
            // 百钱白鸡
            /*
           公鸡每只 5 文钱，母鸡每只 3 文钱，小鸡 3 只 1 文钱。
           现在用 100 文钱 购买 100 只鸡，问：公鸡、母鸡、小鸡各买多少只？*/
            //公鸡
            /*int Roostera = 0;
            int Hen = 0;
            int Chick = 0;
            for (int i = 0; i <= 20; i++)
            {

                Roostera = i * 5;
                for (int j = 0; j <= 33; j++)
                {
                    Hen = j * 3;
                    //小鸡数量
                    Chick = (100 - Roostera - Hen) * 3;
                    if (Chick >= 0 && i + j + Chick == 100)
                    {
                        Console.WriteLine($"公鸡{i}只，母鸡{j}只，小鸡{Chick}只");
                    }

                }
            }*/
            // 1-10000之间被7整除,计算输出每5个和
            /*int Sun = 0;
            for (int i = 1; i <= 10000; i++)
            {
                if (i%7==0 )   
                {
                    Sun += i;
                    if (i % 5 == 0)
                    {
                        Console.WriteLine(Sun);
                        Sun = 0;
                    }
                }
            }*/
        }
        public void TestFunction()
        {
            /* int a = 0;
             //8 
             for (int i = 0; i <= 100; i++)
             {
                 if (i%3==0 && i%5!=0)
                 {
                     a++;
                 }
             }
             Console.WriteLine(a);*/
            //----------  9--------------------
            /*Console.WriteLine("输入5个大写字母");
            string str = Console.ReadLine();
            foreach (char c in str)
            {
                if (c >= 'A' && c <= 'Z')
                {

                }
                else
                {
                    Console.WriteLine("重新输入{0}", c);
                }
            }*/


            //10
            /*Console.WriteLine("输入一个整数");
            while (true)
            {
                int length = int.Parse(Console.ReadLine());
                if (length > 0)
                {
                    for (int i = 1; i < length; i++)
                    {
                        Console.WriteLine(i);
                    }

                }
                else
                {
                    return;
                }
            }*/

            //11

        }
        //
        public int MAx(int i, int j, int k)
        {
            return Math.Max(Math.Max(i, j), k);
        }
        public int Min(int i, int j, int k)
        {
            return Math.Min(Math.Min(i, j), k);
        }
        public void Power(int a, int b)
        {
            int c = 1;
            for (int i = 1; i <= b; i++)
            {
                c = c * a;

            }
            Console.WriteLine(c);
        }
        public void Max(params int[] a)
        {
            foreach (int i in a)
            {
                if (i > a[0])
                {
                    a[0] = i;
                }
            }
            Console.WriteLine(a[0]);
        }
        public void String(string a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(a[i]);
            }
        }
        /// <summary>
        /// 判断回文数
        /// </summary>
        /// <param name="a"></param>
        public void string1(string a)
        {
            int k = a.Length / 2;
            bool flag = true;
            for (int i = 0; i <= k-1; i++)
            {
                if (a[i] != a[a.Length - 1-i])
                {
                    Console.WriteLine(a[i] +"---"+ a[a.Length - 1 - i]);
                    Console.WriteLine(  "不是回文数");
                    flag=false;
                    break;
                }
                
                

            }
            if (flag)
            {
                Console.WriteLine("是回文数");
            }

        }

        /// <summary>
        /// *移动电话号码
        /// </summary>
        /// <param name="a"></param>

        public void MobilePhoneNumber(string a)
        {
            int length = a.Length;
            bool flag = long.TryParse(a, out long j);
            if (length == 11 && flag)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i >= 3&& i < 7)
                    {
                        Console.Write("*");
                        continue;
                    }
                    Console.Write(a[i]);
                }
            }
            else
            {
                Console.WriteLine("请输入11位手机号码");
            }
        }

    }
}

