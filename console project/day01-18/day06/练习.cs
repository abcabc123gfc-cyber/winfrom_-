using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day06
{
    internal class Practice
    {
        //方法创建一个一维数组,初始化数组元素为1-54
        public string[] CreateArray()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Dictionary<int, string> dic = new Dictionary<int, string>
            {
                { 1, "♣" },
                { 2, "♦️" },
                { 3, "❤️" },
                { 4, "♠️" }
            };
            string[] arr = new string[54];
            int temp = 1;
            int Count = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (temp > 13)
                {
                    temp = 1;
                    Count++;
                }

                switch (Count)
                {
                    case 1:
                        arr[i] = temp + dic[1];
                        break;
                    case 2:
                        arr[i] = temp + dic[2];
                        break;
                    case 3:
                        arr[i] = temp + dic[3];
                        break;
                    case 4:
                        arr[i] = temp + dic[4];
                        break;
                    default:
                        if (i == 52)
                        {
                            arr[i] = "小王";
                        }
                        else if (i == 53)
                        {
                            arr[i] = "大王";
                        }
                        break;
                }
                temp++;

            }
            return arr;
        }

        //洗牌的方法
        public string[] Shuffle()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Random r = new Random();
            string[] arr = CreateArray();
            for (int i = 0; i < arr.Length; i++)
            {
                int j = r.Next(arr.Length);
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }

            //foreach (var item in arr)
            //{
            //    Console.WriteLine(item);
            //}
            return arr;
        }

        //创建一个出牌的方法
        public void OutCard()
        {
            string[] arr = Shuffle();
            Console.OutputEncoding = Encoding.UTF8;
            for (int k = 0; k < arr.Length; k++)
            {
                if (k < 17)
                {
                    arr[0] = "玩家 " + "\n\r" + arr[0];
                    Console.Write(arr[k] + " ");
                }
                else if (k < 34)
                {
                    arr[17] = "\n\r" + "电脑1 " + "\n\r" + arr[17];

                    Console.Write(arr[k] + " ");
                }
                else if (k < 51)
                {
                    arr[34] = "\n\r" + "电脑2 " + "\n\r" + arr[34];

                    Console.Write(arr[k] + " ");
                }
                else
                {
                    arr[51] = "\n\r" + "保留牌为" + "\n\r" + arr[52];
                    Console.Write(arr[k] + " ");

                }



            }
        }

        /// <summary>
        /// 创建一个方法 翻转数组
        /// </summary>
        /// <param name="arr"></param>
        public void ArrarReverse(int[] arr)
        {
            int length = arr.Length;
            for (int i = 0; i < length / 2; i++)
            {
                (arr[i], arr[length - 1 - i]) = (arr[length - 1 - i], arr[i]);

            }
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }

        //创建一个方法 随机获取一个1-99的数
        public void GetRandom()
        {
            Random r = new Random();
            int[] num = new int[100];
            for (int i = 0; i < 100; i++)
            {
                num[i] = r.Next(1, 100);
                //Console.WriteLine(num[i]);
                if (num[i] == 30)
                {
                    Console.WriteLine("30出现的位置是{0}", i);
                }
            }

        }

        public void ArrayDisarrange(int[] arr)
        {
            int Length = arr.Length;
            Random r = new Random();
            for (int i = 0; i < Length; i++)
            {
                int index = r.Next(Length);
                (arr[i], arr[index]) = (arr[index], arr[i]);

            }
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
        }

        //冒泡排序
        public void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[j] < arr[i])
                    {
                        (arr[j], arr[i]) = (arr[i], arr[j]);
                    }
                }
            }
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }

        }

        //
    }
    class Peraon
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }


        public Peraon(string name, int age, char sex)
        {
            this.Name = name;
            this.Age = age;
            this.Sex = sex;

        }


        /// <summary>
        /// 获取性别为男的,数组
        /// </summary>
        /// <param name="man"></param>
        /// <returns></returns>
        public string[] GetMan(List<string> man)
        {
            string[] temp = man.Where(x => x == "男").ToArray();
         
            return temp;
        }
        /// <summary>
        /// 获取性别为男的,所有,名字
        /// </summary>
        /// <param name="man"></param>
        public void GetPerson(Peraon[] man)
        {
            foreach (var item in man)
            {
                if (item.Sex == '男')
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        // <summary>
        /// 获取性别为woman的,第一个
        /// </summary>
        public string GetWoman(Peraon[] woman)
        {

            foreach (var item in woman)
            {

                if (item.Sex == '女')
                {
                    return item.Name;
                }
            }

            return default;
        }
        /// <summary>
        /// 判断年龄是否大于18,都
        /// </summary>
        /// <param name="Adult"></param>
        /// <returns></returns>
        public bool IsAdult(Peraon[] Adult)
        {

            foreach (var item in Adult)
            {
                if (item.Age < 18)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取平均年龄
        /// </summary>
        public int GetAgeAvg(Peraon[] ageAvg)
        {
            int sum = 0;
            foreach (var item in ageAvg)
            {
                sum += item.Age;
            }
            return sum / ageAvg.Length;
        }
        /// <summary>
        /// 获取第一个18岁以下的男
        /// </summary>
        public string GetMinor(Peraon[] minor)
        {
            foreach (var item in minor)
            {
                if (item.Age < 18)
                {
                    return item.Name;
                }
            }
            return default;
        }
    } 

    class PracticeTest
    {
        
    }


}
