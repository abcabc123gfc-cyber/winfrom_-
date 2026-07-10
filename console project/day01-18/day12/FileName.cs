using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day12
{
    internal class FileName
    {
    }
  class Student1
    {
        Student student { get; set;}
        public int ID { get; set; }
        public string Name { get; set; }
        public static int Count { get; set; }
        public void Report()
        {
            Console.WriteLine("学号：{0}，姓名：{1}", ID, Name);
            //静成员变量 来代表类的实例个数 学生总数 现实世界的事物
            Count++;
        }
        //Arial Black 这个字体
        //内存泄漏：可以动态使用的内存越来越少
        //栈溢出：内存栈区的使用空间被用光
        ~Student1()
        {
            Console.WriteLine("对象销毁");
        }
    }
}
