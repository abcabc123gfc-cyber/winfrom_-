using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day11
{
    internal class 对象
    {
        // 对象是new出来的, 对象是 引用类型
        // 当创建一个类的时候, 就相当于创建了一个新模版,通过这个类创建的对象, 就是对类的一个实例
        // 类是模板, 对象是实例

        //访问类成员 : 对象名.成员名
        //设置: 对象名.成员名称=值



        public void Demo()
        {
            
            Person p1 = new Person();
            p1.Name = "张三";
            p1.Age = 18;
            p1.Sex = '男';
            p1.Show();
            Person p2 = new Person();
            p2.Name = "张三";
            p2.Age = 18;
            p2.Sex = '男';
            p2.Show();
        }
        
    }
    class Person
    {

        public string Name { get; set;}
        public int Age { get; set; }
        public char Sex { get; set; }
        public void Show()
        {
            Console.WriteLine("Name:{0},Age:{1}",Name,Age);
        }
    }
}
