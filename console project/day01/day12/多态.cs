using System;
using System.Xml.Linq;

namespace day12
{
    internal class 多态
    {
        //多: 同一种行为具有不同的表现形式和能力
        /*
         多态的分类: 
        编译时多态(静态多态): 方法的重载,运算符的重载,索引器的重载
        运行时多态(动动态多态): 1.虚方法 2. 抽象方法 3. 接口
         
        //运算符 
         */
        
    }
    public class Animal
    {
        public string Name { get; set; }
        //多态的实现方式: 1.继承 2.重写 3.抽象类 4.接口
        public int Number { get; set; }
        
        public Animal()
        {
        }
        public static int  operator +( Animal a, Animal b)
        {
            return a.Number + b.Number;
        }

        public Animal(string Name,int Nu)
        {
            this.Name = Name;
            Number=Nu;
        }
        //虚方法重载
        public virtual void Eat(string eat)
        {
            Console.WriteLine(eat);
        }
    }
    public class Cat : Animal
    {
        public Cat()
        {

        }
        public Cat(string Name,int nu) : base(Name,nu)
        {

        }

        public override void Eat(string eat)
        {
            Console.WriteLine($"{Name}吃了{eat}");
        }

    }
    public class Dog:Animal
    {
        public Dog()
        {

        }
        public Dog(string Name, int nu) : base(Name, nu)
        {

        }

        public override void Eat(string eat)
        {
            Console.WriteLine($"{Name}吃了{eat}");
        }
    }
}

