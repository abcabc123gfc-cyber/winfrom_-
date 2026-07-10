using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day16
{
    internal class 接口的继承
    {
    }

    interface IPeople
    {
        string Name { get; set; }
        int Age { get; set; }
        void Show();
    }
    //接口继承接口
    interface IStudent : IPeople
    {
        string School { get; set; }
        void Study();
    }
    //一个类实现另一个接口的时候,如果接口b实现类接口a,那么了必须实现 接口a与接口b 的所有成员
    class Student : IStudent
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string School { get; set; }
        public void Show()
        {
            Console.WriteLine("Name:{0},Age:{1}", Name, Age);
        }
        public void Study()
        {
            Console.WriteLine("Study");
        }
    }
}
