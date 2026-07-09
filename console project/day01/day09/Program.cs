using System;
using System.Collections.Generic;

namespace day09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //动态数组_ArrayList arrayList = new 动态数组_ArrayList();
            //arrayList.ArrrayListDemo();
            practice practice = new practice();
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };

            //practice.ListAvg(list);
            list = practice.CreateListRandom();
            practice.Print_AddAndEven();
            //practice.Remove_List(list);
            //创建一个字符串list
           /* List<string> list1 = new List<string>() { "aaa", "bbb", "ccc", "ppp", "bbb" };
            list1.Sort();
            foreach (var i in list1)
            {
                Console.WriteLine(i);
            }*/

            //航班
            Flight();



        }
        public static void Flight()
        {
            practice practice = new practice();
            List<string> listFlight = new List<string> {
            "001,CZ1001,上海,北京,2020-05-01",
            "002,CZ1002,上海,北京,2020-05-02",
            "003,CZ1003,上海,北京,2020-05-03",
            "004,CZ1004,上海,北京,2020-05-04",
            "005,CZ1005,上海,北京,2020-05-05",
            "006,CZ1006,上海,北京,2020-05-06",
            "007,CZ1007,上海,北京,2020-05-07",
            "008,CZ1008,上海,北京,2020-05-08",
            "009,CZ1009,上海,北京,2020-05-09"
        };
            while (true)
            {
                Console.WriteLine("1.查询所有2.按起飞时间查询3.按目的地查询4.删除航班5.更新航班6.退出");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        practice.flightSelectAll(listFlight);
                        break;
                    case 2:
                        Console.WriteLine("请输入时间查询");

                        practice.flightSelectTime(listFlight, Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("请输入目的地查询");
                        practice.flightSelectCity(listFlight, Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("请输入删除的ID");
                        practice.FlightRemoveID(ref listFlight, Console.ReadLine());
                        break;
                    case 5:

                        practice.FlightUpdate(ref listFlight);
                        break;
                    case 6:
                        return;
                }

            }
        }
    }
}
