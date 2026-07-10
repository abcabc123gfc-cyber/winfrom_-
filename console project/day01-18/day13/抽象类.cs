using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace day13
{
    internal class 抽象类
    {
        //抽象类: 本身也是一个类, 但不能创建对象
        //可以创建 抽象方法与普通方法
        //抽象类: 指被设计出来要被继承的一个类,不能被实例化

        //抽象类与普通类的区别:
        //抽象类:
        //不能被实例化 ,可以有抽象方法与普通方法,可以继承抽象类
        //如果子类不是抽象类,子类必须实例化,需要被子类重写所以抽象类不能使用private 修饰符
        //普通类:
        //可以被实例化,不能有抽象方法只能有普通方法,可以继承抽象类,但需要对抽象类中抽象方法重写

        //使用 override 重写抽象方法


    }

    //抽象类的声明和 普通类一样 只需要在类前加上 abstract 关键字
    abstract class Printer
    {
        public string Name { get; set;}
        public int Price { get; set;}
        //抽象类中可以定义抽象方法
        //给某个方法前面加上 abstract 关键字,可以把这个方法修饰成抽象方法
        //抽象方法: 不能又给有内容 方法体

        abstract public void Print(string value);
        abstract public void Fn();
        virtual public void Show()
        {
            
        }

    }
    class ColorPrinter : Printer
    {
      public override void Print(string value)
      {
          Console.WriteLine("打印{0}",value);
      }
        public override void Fn()
        {
           
        }
    }
   
}
