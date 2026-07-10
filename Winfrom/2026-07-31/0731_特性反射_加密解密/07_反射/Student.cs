using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_反射
{
    public class Student
    {


        public class SamllStudent
        {
            public string Name { get; set; } = "小学生";
            public class AAA { }
        }


        //字段
        public int myId = 10;
        public string myName = "吴亦凡";


        private int _id;


        //属性
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string name;

        //属性
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        [Obsolete("过时的方法Test1")]
        public void Test1(string a,string b)
        {
            Console.WriteLine("Test1"+a+b);
        }
        public void Test2()
        {
            Console.WriteLine("Test2");
        }

    }
}
