using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Practice
{
    internal class prictice
    {
        /// <summary>
        /// 27 三角形
        /// </summary>
        public void triangle()
        {
            for (int i = 10; i > 0; i--)
            {
                for (int j = i; j > 0; j--)
                {
                    Console.Write(" ");
                }
                for (int k = 10 - i; k >= 0; k--)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }
        #region 日历 35
        public void Calendar()
        {
            int Year = int.Parse(Console.ReadLine());
            string[][] str = new string[12][];
            for (int i = 0; i < 12; i++)
            {
                str[i] = new string[MonthDays(Year, i + 1)];
                for (int j = 0; j < MonthDays(Year, i + 1); j++)
                {
                    str[i][j] = j + "日";
                    Console.Write(str[i][j]);
                }
                Console.WriteLine();
            }
        }
        public int MonthDays(int year, int month)
        {
            if (month == 2)
            {
                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                {
                    return 29;
                }
                else
                {
                    return 28;
                }
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            else
            {
                return 31;
            }
        }
        #endregion

        /// <summary>
        /// 省市38
        /// </summary>
        public void Sheng()
        {
            string str = Console.ReadLine();
            Dictionary<string, List<string>> provinces = new Dictionary<string, List<string>>();
            provinces.Add("广东", new List<string> { "广州", "深圳", "珠海" });
            provinces.Add("浙江", new List<string> { "杭州", "宁波", "温州" });
            provinces.Add("江苏", new List<string> { "南京", "苏州", "无锡" });
            foreach (var province in provinces.Keys)
            {
                if (province == str)
                {
                    foreach (var city in provinces[province])
                    {
                        Console.WriteLine("\t" + city);
                    }
                }
            }
        }
        /// <summary>
        /// 车票39
        /// </summary>
        public void Ticket()
        {
            string[,] tickets = new string[4, 25];
            int k = 1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    tickets[i, j] = k.ToString("D3") + "空";
                    k++;
                }
            }
            k = 0;
            foreach (var item in tickets)
            {
                k++;
                if (k % 5 == 0)
                {
                    Console.WriteLine(item);
                }
                else
                {

                    Console.Write(item + "\t");
                }

            }
            int n = int.Parse(Console.ReadLine());
            string s = tickets[n / 25, n % 25 - 1].Replace("空", "已售");
            Console.WriteLine(s);
            tickets[n / 25, n % 25 - 1] = s;
            foreach (var item in tickets)
            {
                k++;
                if (k % 5 == 0)
                {
                    Console.WriteLine(item);
                }
                else
                {
                    Console.Write(item + "\t");
                }
            }
        }

        #region 舞伴配对问题
        public void DancePartner()
        {
            List<string> man = new List<string>()
            {
                "张三","李四","王五","赵六","孙七"
            };
            List<string> woman = new List<string>()
            {
                "小红","小丽","小芳"
            };
            List<string> pair = new List<string>();

            //等待列表
            List<string> wait = new List<string>();

            //场次
            int count = 1;
            //男方 人次_用于查找谁没有配对
            int k = 0;
            int number = 0;

            //进入循环
            while (true)
            {
                //初始化配对人次
                for (int i = 0; i < woman.Count; i++)
                {
                    pair.Add(man[k++] + "" + " " + woman[i] + "");

                    if (k == man.Count)
                    {
                        k = 0;
                    }


                }
                //多增加两次循环
                number = k;
                for (int p = 0; p < 2; p++)
                {
                    if (number == man.Count)
                    {
                        number = 0;
                        wait.Add(man[number]);
                        number++;
                    }
                    else
                    {
                        wait.Add(man[number]);
                        number++;
                    }
                }


                Console.WriteLine($"第{count++}场舞曲开始");
                //配对
                for (int i = 0; i < pair.Count; i++)
                {
                    Console.WriteLine(pair[i] + " 配对完成开始跳舞");
                }
                //遍历等待队列_男士
                foreach (var item in wait)
                {
                    Console.WriteLine(item + " 等待舞伴");
                }
                wait.Clear();
                Console.WriteLine("歌曲停止");
                //删除以配对的舞伴,并进入等待队列
                foreach (var item in pair)
                {

                    Console.WriteLine(item + "进入循环等待队列");

                }
                //清空
                pair.Clear();
                Console.ReadKey();
            }
        }
        #endregion`

        #region 括号匹配 41
        /// <summary>
        /// 括号匹配 41
        /// </summary>
        public void StackDeomo()
        {
            Console.WriteLine("输入表达式字符");
            string str = Console.ReadLine();
            int temp = 0;
            foreach (var item in str)
            {
                if (item == '<' || item == '{' || item == '(' || item == '[')
                {
                    temp++;
                }
                else if (item == '>' || item == '}' || item == ')' || item == ']')
                {
                    temp--;
                }
            }
            Console.WriteLine(temp == 0 ? "括号匹配" : "括号不匹配");

        }
        #endregion

        /// <summary>
        /// 古诗分行 42
        /// </summary>
        public void Poem()
        {
            string poetry = "日照香炉生紫烟，遥看瀑布挂前川。飞流直下三千尺，疑是银河落九天。";
            foreach (var item in poetry)
            {
                if (item == '，' || item == '。')
                {
                    Console.WriteLine(item);
                }
                else
                {
                    Console.Write(item + " ");
                }
            }
        }

        /// <summary>
        /// 判断古诗输入是否正确 43
        /// </summary>
        public void PoemIsTrue()
        {
            string[] peoms = "日照香炉生紫烟，遥看瀑布挂前川。飞流直下三千尺，疑是银河落九天".Split('，');
            foreach (var item in peoms)
            {
                string str = Console.ReadLine();
                if (str.Equals(item))
                {
                    Console.WriteLine("输入正确");
                }
                else
                {
                    Console.WriteLine("输入错误");
                }
            }

        }
        /// <summary>
        /// 字符格式
        /// </summary>
        public void InoutFormat()
        {
            //货币格式
            Console.WriteLine(
                "货币格式" + 12334.ToString("C"));
            //十进制格式
            Console.WriteLine("十进制格式" + 12334.ToString("D"));
            //十六进制格式
            Console.WriteLine("十六进制格式" + 12334.ToString("X"));
            //科学计数法格式
            Console.WriteLine("科学计数法格式" + 12334.ToString("E"));
            //浮点数格式
            Console.WriteLine("浮点数格式" + 12334.ToString("F"));
            //百分比格式
            Console.WriteLine("百分比格式" + 12334.ToString("P"));
            //千分位格式
            Console.WriteLine("千分位格式" + 12334.ToString("N"));
        }


        /// <summary>
        /// 判断ip地址是否正确 52
        /// </summary>
        public void IPIsTrue()
        {
            Console.WriteLine("输入ip地址");
            string ip = Console.ReadLine();
            if (ip.Split('.').Length == 4)
            {
                string[] strings = ip.Split('.');
                foreach (var item in strings)
                {
                    if (int.Parse(item) >= 0 && int.Parse(item) <= 254)
                    {
                    }
                    else
                    {
                        Console.WriteLine("ip地址错误");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("输入错误");
                return;
            }
            Console.WriteLine("输入正确");
        }

        /// <summary>
        /// base64字符串转换 54
        /// </summary>
        public void BaseStrig()
        {
            
            string str = "123456789";
            //转为base64字符串  对字符串进行编码 ToBase64String
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string str1= Convert.ToBase64String(bytes);
            Console.WriteLine(str1);
            //base64字符串转为原始字符串 对字符串进行解码 FromBase64String
            byte[] bytes1 = Convert.FromBase64String(str1);
            Console.WriteLine(Encoding.UTF8.GetString(bytes1));
        }

        //
        public string Call1(string str)
        {
            return str;
        }
       
        

    }

    /// <summary>
    /// 索引器 77
    /// </summary>
    public class Student
    {
        public int[] Scour;

        public Student(params int[] value)
        {
            Scour = value;
        }


        public int this[int index]
        {
            get
            {
                int num = 0;
                for (int i = 0; i < Scour.Length; i++)
                {
                    num += Scour[i];
                }
                return num / Scour.Length;
            }
            set => Scour[index] = value;
        }

    }
}
