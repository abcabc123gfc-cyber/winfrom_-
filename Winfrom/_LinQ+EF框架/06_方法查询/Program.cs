using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace _06_方法查询
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //linq 除了可以使用语句(表达式) 查询之外, 还可以使用方法查询
            int[] ints = new int[] { 12, 34, 5, 6, 7, 2, 4, 56, 52, 323, 234 };

            //ToArray() Tolist() 都是Linq提供的方法 
            //ToArray() 的作用就是将查询道德结果转换为Array 
            int[] a= (from v in ints select v).ToArray();

            List<int> b= (from v in ints select v).ToList();

            int[] c= ints.Select(v=>v).ToArray();
            bool[] d=ints.Select(v=>v%2==0).ToArray();

            List<Student> students = new List<Student>()
            {
                new Student(){StuName="吴亦凡",RoomId=1},
                new Student(){StuName="罗志祥",RoomId=1},
                new Student(){StuName="李云迪",RoomId=2},
                new Student(){StuName="李易峰",RoomId=3},
                new Student(){StuName="蔡徐坤",RoomId=3},
            };

            //select 查询 
            Student[] students1 = (from s1 in students select s1).ToArray();

            string[] names = (from s1 in students select s1.StuName).ToArray();
            string[] names2=students.Select(m=>m.StuName).ToArray();

            foreach (var item in names)
            {
                Console.WriteLine(item);
            }

            List<User> users = new List<User>()
            {
                new User(){UserId="1",UserName="吴亦凡",UserPhone="4234324234",Age=20},
                new User(){UserId="2",UserName="罗志祥",UserPhone="4234123324234",Age=10},
                new User(){UserId="3",UserName="李云迪",UserPhone="1232",Age=20},
                new User(){UserId="4",UserName="李易峰",UserPhone="213",Age=30},
            };
            
            //where 过滤
            List<User> u1 =(from u4 in users where u4.Age>20 select u4).ToList();

            List<User> u2 =users.Where(m=>m.Age>20).ToList();

            foreach (var item in u2)
            {
                Console.WriteLine(item.UserName);
            }
            //groupby 分组 根据某个字段进行分组
            var s3 = students.GroupBy(v => v.RoomId);

            int[] intse = new int[] { 12, 34, 5, 6, 7, 2, 4, 56, 52, 323, 234 };

            //All() 判断所有元素是否满足条件. 都满足返回true
            intse.All(v => v > 10);
            //Any() 判断是否有元素满足条件. 有一个满足返回true
            intse.Any(v => v > 10);

            //Count() 统计元素个数
            intse.Count();
            intse.Count(v => v > 10);
            //去除重复元素
            intse.Distinct();
            intse.Distinct().ToArray();
            //获取最大值
            intse.Max();
            intse.Max(v => v);
            //获取最小值
            intse.Min();
            intse.Min(v => v);
            //求和
            intse.Sum();
            intse.Sum(v => v);
            //求平均值
            intse.Average();
            intse.Average(v => v);
            //获取指定索引的未知元素
            intse.ElementAt(2);

            //有索引值, 为什么还需要ElementAt() 方法原因:
            //因为where方法与select方法, 都会返回一个集合,没有索引器, 需要使用 ElementAt()获取值

            //获取第一个满足条件的元素
            intse.First();
            intse.First(v => v > 10);
            intse.FirstOrDefault();
            //获取最后一个满足条件的元素
            intse.Last();
            intse.Last(v => v > 10);
            intse.LastOrDefault();
            //翻转集合
            intse.Reverse();
            //排序
            intse.OrderBy(v => v);
            //降序排序
            intse.OrderByDescending(v => v);

            
        }

    }
}
