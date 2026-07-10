using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_groupby子句
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scores = new int[] { 90, 10, 100, 80, 10, 30, 20, 34, 41, 12 };
            //group by 子句 用来分组
            //与sql 语句一中的 group  by 一样用来分组
            //格式: from x in y group 分组的内容 by 分组的条,组名称;

            //获取数据结果的时候 得到是多个组, 循环每个组, 可以得到每个组的信息
            IEnumerable<IGrouping<bool, int>> Result = from s1 in scores group s1 by s1 > 60;
            //foreach (var item in Result)
            //{
            //    //循环每个组
            //    //item  每个组的集合, 每个组都会有一个bool类型的key 属性, 存储了当前分组的标识
            //    //满足分组条件的为true  不满足分组条件的为false
            //    //Console.WriteLine(item.Key);
            //    string str = item.Key ? "及格" : "不及格";
            //    foreach (var i in item)
            //    {
            //        Console.WriteLine(str+i);
            //    }
            //}

            List<User> users = new List<User>()
            {
                new User(){UserId="1",UserName="吴亦凡",UserPhone="110",Age=20},
                new User(){UserId="2",UserName="罗志祥",UserPhone="213",Age=10},
                new User(){UserId="3",UserName="李云迪",UserPhone="213",Age=20},
                new User(){UserId="4",UserName="李易峰",UserPhone="213",Age=30},
            };

            IEnumerable<IGrouping<string, User>> users1 = from u1 in users group u1 by u1.UserPhone;
            //foreach (var item in users1)
            //{
            //    //item  每个组的集合, 每个组都会有一个string类型的key 存储了当前分组的标识
            //    //Console.WriteLine(item.Key);
            //    string str = "组名称"+item.Key;
            //    foreach (var i in item)
            //    {
            //        Console.WriteLine(str+i);
            //    }
            //}
            IEnumerable<IGrouping<string, string>> strings = from s1 in users group s1.UserName by s1.UserPhone;
            foreach (var item in strings)
            {
                //item  每个组的集合, 每个组都会有一个string类型的key 存储了当前分组的标识
                //Console.WriteLine(item.Key);
                string str = "组名称" + item.Key;
                foreach (var i in item)
                {
                    Console.WriteLine(str + i);
                }
            }
        }
    }
}
