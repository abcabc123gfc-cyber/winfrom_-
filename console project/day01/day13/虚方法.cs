using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day13
{
    public  class Parent
    {
        //声明虚方法: 给某个方法添加一个virtula 关键字
        public virtual void Test()
        {
            Console.WriteLine("虚方法");
        }
    }
    class Child : Parent
    {
        public override void Test()
        {   
            Console.WriteLine("子类重写父类的虚方法");
        }
    }
}
