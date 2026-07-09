using System;
using System.Collections.Generic;

namespace day09
{
    internal class practice
    {
        public void ListAvg(List<int> list)
        {
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            Console.WriteLine(sum / list.Count);

        }

        public List<int> CreateListRandom()
        {
            List<int> list = new List<int>();
            Random random = new Random();
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

            }
            //Print_List(list);
            return list;
        }

        public void Print_List(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
        }

        //
        public void Print_AddAndEven()
        {
            List<int> list = CreateListRandom();
            int Even = 0;
            int temp_Add = 0;
            int temp = 0;
            foreach (var item in list)
            {
                temp++;
                Even++;
                if (temp % 2 != 0 && temp != 1)
                {
                    Console.WriteLine();

                }

                if (item % 2 == 0)
                {
                    // 第一个数是偶数是时
                    if (temp == 1) Console.Write(" " + item + "\n\r ");


                    temp_Add++;
                    if (temp_Add == 2)
                    {
                        temp_Add = 0;
                        Console.Write(item + "\n\r");
                    }
                    Console.Write(" " + item + "\n\r");


                }
                else
                {
                    if (Even == 1)
                    {
                        Console.WriteLine();
                        Even = 0;
                        temp_Add++;
                    }
                    Console.Write(item + " ");
                    Even++;
                    temp_Add = 0;
                }
            }
            Console.WriteLine("\n\r对照");
            foreach (var item1 in list)
            {
                Console.Write(item1 + " ");
            }
        }

        public void Remove_List(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {

                for (int j = 0; j < i; j++)
                {

                    if (list[j] == list[i])
                    {
                        list.RemoveAt(i);

                        break;
                    }
                }

            }
            foreach (var item in list)
            {
                Console.Write(item);
            }
        }



        /// <summary>
        /// 查询所有航班
        /// </summary>
        /// <param name="list"></param>
        public void flightSelectAll(List<string> list)
        {
            Console.Clear();
            Console.WriteLine("ID\t编号\t起始地\t目的地\t时间\t");
            string str;
            for (int i = 0; i < list.Count; i++)
            {
                str = list[i];
                //str.Replace(",", "\t");
             
              str =  str.Replace(",", "\t");
                Console.WriteLine(str);
            }

        }

        /// <summary>
        /// 查询指定城市的航班
        /// </summary>
        /// <param name="list"></param>
        /// <param name="city"></param>
        public void flightSelectCity(List<string> list, string city)
        {
            Console.WriteLine("查询结果_根据城市查询");

            List<string> strlist = list.FindAll(x => x.Contains(city));
            flightSelectAll(strlist);
        }
        /// <summary>
        /// 查询指定时间的航班
        /// </summary>
        /// <param name="list"></param>
        /// <param name="time"></param>
        public void flightSelectTime(List<string> list, string time)
        {
            Console.WriteLine("查询结果_根据时间查询");

            string str;
            List<string> strlist = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                str = list[i];

                if (str.Contains(time))
                {
                    strlist.Add(str);
                    flightSelectAll(strlist);
                    return;
                }

            }

        }

        /// <summary>
        /// 删除指定ID的航班
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        public void FlightRemoveID(ref List<string> list, string id)
        {
            List<string> strlist = list.FindAll(x => x.Contains(id));
            list.RemoveAll(x => x.Contains(id));
            Console.WriteLine("已删除航班信息");
            flightSelectAll(strlist);

        }

        public void FlightUpdate(ref List<string> list)
        {
            Console.WriteLine("请输入航班信息_编号&航班号&目的地&起飞日期以\",\"断字");
            string str = Console.ReadLine();
            list.Add(str);
            Console.WriteLine("更新信息");
            flightSelectAll(list);
        }
    }
}


