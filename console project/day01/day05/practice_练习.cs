using System;
using System.Collections.Generic;

namespace day05
{
    internal class practice_练习
    {
        /// <summary>
        /// 交换两个变量
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void ExchangeVariable(ref int a, ref int b)
        {
            (a, b) = (b, a);
            Console.WriteLine("a={0},b={1}", a, b);
        }

        /// <summary>
        /// 输入1-100的数字，被3的整除的数字
        /// </summary>
        public void Input()
        {
            //1-100 被3的整除的数字

            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0)
                {
                    Console.Write(i + " ");
                }
            }
        }

        /// <summary>
        /// 输出水仙花数
        /// </summary>
        public void NarcissisticNumber()
        {
            for (int i = 100; i <= 999; i++)
            {
                int nuitsDigit = i % 100;
                int tensDigit = i % 10 / 10;
                int hundredsDigit = i / 100;
                if (i == nuitsDigit * 100 + tensDigit * 10 + hundredsDigit)
                {
                    Console.WriteLine(i + " 是水仙花数");
                }
            }
        }

        /// <summary>
        /// 输出乘法表 goto
        /// </summary>
        public void multiplicationTable()
        {
            int i = 0, j = 0, Index = 0;
        tbale:
            i++;
        table1:
            Index++;
            j++;
            if (i >= Index)
            {
                Console.Write($"{i}*{j}={i * j}\t");
                goto table1;
            }
            Index = 1;
            j = 1;
            Console.WriteLine();
            if (i >= 9) return;

            goto tbale;
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void Loginng()
        {

            string newName = ""; string newPwd = "";
            while (true)
            {
                Console.Clear();

                Console.WriteLine("1 注册,2登录,3退出");
                switch (Console.ReadLine())
                {
                    case "1":

                        Console.WriteLine("输入名字");
                        newName = Console.ReadLine();
                        Console.WriteLine("输入密码");
                        newPwd = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("请输入用户名：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入密码：");
                        string pwd = Console.ReadLine();

                        if (name != newName || pwd != newPwd)
                        {
                            Console.WriteLine("用户名或密码输入错误！");
                            Console.WriteLine("请重新输入用户名：");
                            name = Console.ReadLine();
                            Console.WriteLine("请重新输入密码：");
                            pwd = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("登录成功");
                            Console.ReadKey();
                        }

                        break;
                    case "3":
                        Console.WriteLine("退出");
                        return;
                    default:
                        Console.WriteLine("无效");
                        break;
                }
            }


        }

        /// <summary>
        /// 螺钉和螺母,计算
        /// </summary>
        public void Screw()
        {
            for (int i = 1; i <= 26; i++)
            {
                if (i * 800 == (26 - i) * 1000 / 2)
                {
                    Console.WriteLine($"{i * 800},::{(26 - i) * 1000 / 2}");
                    Console.WriteLine($"螺钉工人{i},螺母工人{26 - i}");
                }
            }
        }

        public void JIaYi()
        {
            // 假设 甲 是 x 
            //乙 是 y   x*1.2+y*1.15=680+118;, x+y=680; 设x的产量是 x=680-y; y= (680+118-x*1.2)/1.15;
            for (int i = 1; i <= 400; i++)
            {
                if (i == 680 - (680 + 118 - i * 1.2) / 1.15)
                {
                    Console.WriteLine($"甲的产量是{i},乙的产量是{680 - i}");
                }
            }
        }

        /// <summary>
        /// 找数
        /// </summary>
        public void FindNumber()
        {
            for (int i = 10; i < 100; i++)
            {
                int nuitsDigit = i % 10;
                int tensDigit = i / 10;
                if (nuitsDigit + tensDigit == 11 && (nuitsDigit * 10 + tensDigit) - i == 63)
                {
                    Console.WriteLine(i);
                }
            }
        }
        /// <summary>
        /// 火车通过时间
        /// </summary>
        public void trainTunnelTraining()
        {
            for (int i = 1; i < 400; i++)
            {
                if ((300 + i) / 20.0 * 10.0 == i)
                {

                    Console.WriteLine("火车长度是{0},速度为{1}", i, ((300 + i) / 20));
                }
            }
        }

        /// <summary>
        /// 水费
        /// </summary>
        public void waterBill()
        {
            int a = 0;
            for (int i = 1; i < 100; i++)
            {
                a = i * 3;
                if (20 * 2 + a == 64)
                {
                    Console.WriteLine("用水为{0}立方米", i + 20);
                }
            }
        }

        public void NestedLoopLogin()
        {
            //while 循环 Loginng();
            //do while
            string newName = ""; string newPwd = "";
            int i = 1;
            do
            {

                Console.Clear();

                Console.WriteLine("1 注册,2登录,3退出");
                switch (Console.ReadLine())
                {
                    case "1":

                        Console.WriteLine("输入名字");
                        newName = Console.ReadLine();
                        Console.WriteLine("输入密码");
                        newPwd = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("请输入用户名：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入密码：");
                        string pwd = Console.ReadLine();

                        if (name != newName || pwd != newPwd)
                        {
                            Console.WriteLine("用户名或密码输入错误！");
                            Console.WriteLine("请重新输入用户名：");
                            name = Console.ReadLine();
                            Console.WriteLine("请重新输入密码：");
                            i++;
                            if (i > 3)
                            {
                                Console.WriteLine("输入错误次数过多");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("登录成功");
                            Console.ReadKey();
                        }

                        break;
                    case "3":
                        Console.WriteLine("退出");
                        return;
                    default:
                        Console.WriteLine("无效");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// 年份判断 加差值判断
        /// </summary>
        public void YearJudgment()
        {
            List<int> list = new List<int>();
            for (int i = 1; i < 2100; i++)
            {
                if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                {

                    list.Add(i);
                }
            }
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i - 1] - list[i] == 4)
                {
                    list.RemoveAt(i);
                }
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// 鸡兔同笼
        /// </summary>
        public void JiTu()
        {
            for (int i = 1; i <= 35; i++)
            {
                if (i * 2 + ((35 - i) * 4) == 94)
                {
                    Console.WriteLine("{0},{1}", i * 2, (35 - i) * 4);
                    Console.WriteLine($"鸡有{i}只,兔子有{35 - i}");
                }
            }
        }

        /// <summary>
        /// 三种乘法口诀写法
        /// </summary>
        public void multiplicationTableThread()
        {
            //goto写法multiplicationTable();
            //for写法
            //for (int i = 1; i <= 9; i++)
            //{
            //    for (int j = 1; j <= i; j++)
            //    {
            //        Console.Write($"{j}*{i}={i * j}\t");
            //    }
            //    Console.WriteLine();
            //}

            //while 写法 
            int i = 0, j = 0, Index = 0;
            while (true)
            {
                i++;
                while (true)
                {
                    Index++;
                    j++;
                    if (i >= Index)
                    {
                        Console.Write($"{i}*{j}={i * j}\t");

                    }
                    else
                    {
                        //Console.WriteLine("结束内层循环");
                        Index = 0;
                        j = 0;
                        break;
                    }

                }
                Console.WriteLine();
                if (i >= 9)
                {


                    break;
                }
            }
        }

        /// <summary>
        /// 衬衫利润
        /// </summary>
        public void t()
        {
            for (int i = 1; i <= 140; i++)
            {
                if ((i * 25 + (140 - i) * 20) - (i * 10 + (140 - i) * 8) == 1860)
                {
                    Console.WriteLine("白{0},黑{1}", (140 - i), i);
                }
            }
        }

        // A +B=70; 3A*1.1+2B*0.95=175;
        /// <summary>
        /// 服装价格
        /// </summary>
        public void Clothing()
        {
            for (int i = 1; i <= 175; i++)
            {
                if (3 * (70 - i) * 1.1 + 2 * i * 0.95 == 175)
                {
                    Console.WriteLine($"A服装{70 - i},B服装{i}");
                }
            }
        }

        /// <summary>
        /// 日期计算
        /// </summary>
        public void LeapYear()
        {
            Console.WriteLine("输入年份");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("输入月份");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("输入日期");
            int day = Convert.ToInt32(Console.ReadLine());


            for (int i = month + 1; i <= 12; i++)
            {
                day += monthDays(year, i);
            }
            Console.WriteLine(day);

        }

        public int monthDays(int year, int month)
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            else
            {
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
        }

        /// <summary>
        /// 关卡分数
        /// </summary>
        public void Game()
        {
            Console.WriteLine("输入关卡数量");
            int level = Convert.ToInt32(Console.ReadLine());
            int score = 0;
            for (int i = 1; i <= level; i++)
            {
                if (i <= 20)
                {
                    score += i;
                }
                if (i > 20 && i <= 30)
                {
                    score += 10;
                }
                if (i > 30 && i <= 40)
                {
                    score += 20;
                }
                if (i > 40 && i <= 49)
                {
                    score += 30;
                }

                if (i == 50)
                {
                    score += 100;
                }
            }
            Console.WriteLine("成绩为{0}", score);
        }

        /// <summary>
        /// 100元钱买3个物品
        /// </summary>
        public void shaping()
        {
            int a = 20 + 2 + 5;
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    int price = 100 - a - i * 20 - j * 5;

                    if ((100 - price) % 2 == 0 && price >= 0)
                    {

                        Console.WriteLine($"洗发水{i + 1}块，香皂{j + 1}块，牙刷{(100 - price) / 2 + 1}块");
                    }
                }
            }
        }

        /// <summary>
        /// 百鸡问题
        /// </summary>
        public void BaiJi()
        {
            for (int i = 0; i <= 50; i++)
            {
                for (int j = 0; j <= 100; j++)
                {
                    int money = 100 - i * 2 - j;
                    if (money >= 0 && i + j + money * 2.0 == 100)
                    {
                        Console.WriteLine($"公鸡{i}只，母鸡{j}只，小鸡{money * 2}只");
                    }
                }
            }
        }

        /// <summary>
        /// 数学题,3未知数
        /// </summary>
        public void shapeMath()
        {
            for (int i = 0; i <= 20; i++)
            {
                for (int j = 0; j <= 20; j++)
                {
                    for (int k = 0; k <= 20; k++)
                    {
                        if (2 * i + j + k == 17 && i + 2 * j + k == 14 && i + j + 2 * k == 13)
                        {
                            Console.WriteLine($"方框: {i} 三角: {j}圆形:  {k}");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 特殊三位数
        /// </summary>
        public void ThreadNumber()
        {
            //for循环100-999
            for (int i = 100; i <= 999; i++)
            {
                //百位十位 个位
                int units = i % 10;
                int tens = i / 10 % 10;
                int hundreds = i / 100;
                if (hundreds + tens + units == 17 && (hundreds + tens) - units == 3 && (units * 100 + tens * 10 + hundreds) - i == 495) Console.WriteLine(i);
            }
        }

        /// <summary>
        /// 商品价格计算
        /// </summary>
        public void Goods()
        {
            //三重for循环,每一冲循环80次
            for (int i = 0; i < 140; i++)
            {
                for (int j = 0; j < 140; j++)
                {
                    for (int k = 0; k < 140; k++)
                    {
                        if (i * 5 + j * 2 + k * 4 == 80 && 3 * i + 6 * j + 4 * k == 144)
                        {
                            Console.WriteLine($"{i},{j},{k}");
                        }
                    }
                }
            }
        }

        
    }
}
