using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_select子句
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User(){UserId="1",UserName="吴亦凡",UserPhone="4234324234",Age=20},
                new User(){UserId="2",UserName="罗志祥",UserPhone="4234123324234",Age=20},
                new User(){UserId="3",UserName="李云迪",UserPhone="1232",Age=20},
                new User(){UserId="4",UserName="李易峰",UserPhone="213",Age=30},
            };
            //select 子句: 基于查询的结果, 返回需要的值或者字段, 并且能够对返回值,指定类型, 对任意想要查询的结果的linq语句, 必须select 或者group结尾, 

            var a1 = from u1 in users select u1.UserName;

            var b = from u2 in users select $"u2姓名{u2.UserName}";
            //返回 bool集合
            var c = from u3 in users select u3.Age > 19;

            //new { x=x}  这种写法相当于创建了一个对象, 到那时这个对象不属于任务一个类, 没有类的对象
            //作用: 让一个变量可以存储多个变量的值
            var d = from u4 in users
                    select new
                    {
                        ua = u4.Age,
                        ub = u4.UserName,
                        uc = u4.Age > 19 ? "大于19" : "小于19",
                    };
            foreach (var item in d)
            {
                Console.WriteLine(item.ua);
                Console.WriteLine(item.ub);
                Console.WriteLine(item.uc);
            }

            //将查询到的数据 转换为 People类型, 并且设置conuntry 字段的值为"中国"
            IEnumerable<People> peoples = from u5 in users
                                          select new People()
                                          {
                                              UserId = u5.UserId,
                                              UserName = u5.UserName,
                                              UserPhone = u5.UserPhone,
                                              Country = "中国",

                                          };
            //转换为list<People>
            List<People> peopleList = (from u6 in users
                                       select new People()
                                       {
                                           UserId = u6.UserId,
                                           UserName = u6.UserName,
                                           UserPhone = u6.UserPhone,
                                           Country = "中国",

                                       }).ToList();



        }
    }
}
