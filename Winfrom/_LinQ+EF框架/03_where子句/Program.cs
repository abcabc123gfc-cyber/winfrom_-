using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_where子句
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User(){UserId="1",UserName="吴亦凡",UserPhone="4234324234",Age=20},
                new User(){UserId="2",UserName="罗志祥",UserPhone="4234123324234",Age=10},
                new User(){UserId="3",UserName="李云迪",UserPhone="1232",Age=20},
                new User(){UserId="4",UserName="李易峰",UserPhone="213",Age=30},
            };
            // where 子句: 用来筛选条件 , 和sql查询语句中的where 子句一样, 用来制定筛选条件

            var a = from u1 in users
                    where u1.Age > 20
                    select u1;
            //Console.WriteLine(a.GetType().FullName);

            var a1 = from u2 in users
                     where u2.UserName == "w"
                     select u2;
            var a2 = from u3 in users
                     where u3.Age > 20
                     select u3;

            //orderby 用来排序 与sql查询语句中的orderby 子句一样 用来排序 
            var a3 =from u4 in users
                    //where u4.Age > 20
                    //orderby u4.Age 默认情况下是升序
                    //descending 降序
                    //对于字符字段排序规则: 根据首字母排序,首字母相同按照第二字母排序
                    orderby u4.Age , u4.UserName
                    select u4;
            foreach (var item in a3)
            {
                Console.WriteLine(item.UserName);
            }
        }
    }
}
