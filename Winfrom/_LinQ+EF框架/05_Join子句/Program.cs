using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Join子句
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StuName="吴亦凡",RoomId=1},
                new Student(){StuName="罗志祥",RoomId=1},
                new Student(){StuName="李云迪",RoomId=2},
                new Student(){StuName="李易峰",RoomId=3},
                new Student(){StuName="蔡徐坤",RoomId=3},
            };

            List<ClassRoom> rooms = new List<ClassRoom>()
            {
                new ClassRoom(){Id=1,ClassroomName="一班",Address="金水"},
                new ClassRoom(){Id=2,ClassroomName="二班",Address="二七"},
                new ClassRoom(){Id=3,ClassroomName="三班",Address="经开"},
            };

            var a = from s in students
                    //相当于数据库的连接 
                    //连接rooms 数据源, 连接条件: 表字段相等
                    //连接成功后生成一个新的对象, 包含 s与 r 的所有属性
                    join r in rooms on s.RoomId equals r.Id
                    select new
                    {
                        student = s,
                        root = r
                    };
            foreach (var item in a)
            {
                //类型名称
                Console.WriteLine(item);
                Console.WriteLine(item.student.StuName+" "+ item.root.Id);
            }

            //select 输出字符串
            var a1= from s in students join r in rooms on s.RoomId equals r.Id select $"{s.StuName}在{r.ClassroomName}";
            foreach (var item in a1)
            {
                Console.WriteLine(item);
            }


        }
    }
}
