using System;

namespace day12
{
    internal class 继承类的构造函数
    {
        //当创建子类时,父类就应该存在,因此会先执行父类的构造函数,然后执行子类的构造函数
        //初始化父类构造函数 base(参数)
        //base :表示父类  this 表示的子类 当前类
    }
    class People
    {
        public int Id { get; set; }

        public People()
        {
            Console.WriteLine("无参构造函数people");
        }
        public People(int id) {

            Console.WriteLine("父类有参构造函数");
        }

    }

    class Man : People
    {
        //继承类的构造函数
        public Man():base()
        {
            Console.WriteLine("Student()");
        }
        public Man(int a) :base(a)
        {
            Id = a;

            Console.WriteLine("Student(int a){0}",Id);
        } 

    }
}
