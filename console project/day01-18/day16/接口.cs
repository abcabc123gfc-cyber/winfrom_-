using System;

namespace day16
{
    internal class 接口
    {
        //接口: 是一组成员,而不他们的成员进行实现的引用类型, 只能被类和结构体所实现,类和结构体实现这个接口的时候,必须实现接口中定义的方法

        //接口: 规则 指定应该有什么
        //类和结构体: 实现规则,具体应该怎么有
        //接口定义在命名空间中
        //使用 Interface 定义 接口的名字一般以 I 开头

        //格式: public interface 接口名 { 抽象方法定义; }
        //作用: 1. 类的继承
        //     2. 接口实现

        //当类继承父类 同时又继承接口时, 同名的属性和方法,会从父类继承,不需要子类 强制 实现

        //在类的实现中,继承接口,必须实现接口中所有抽象方法,也可写属于子类特有的方法

    }

    public interface IAnimal
    {
        string Name { get; set; }
        int MaxAge { get; set; }
        void Eat();
        void Sleep();
    }
    public interface ITest
    {
        void Show();
        void Test();
    }
    class Dog : IAnimal
    {
        private string name;
        public string Name { get => name; set => name = value; }
        private int maxAge;
        public int MaxAge { get => maxAge; set => maxAge = value; }

        public void Eat()
        {
            Console.WriteLine();
        }

        public void Sleep()
        {
            Console.WriteLine("");
        }
    }
    public interface ITemp : ITest, IAnimal
    {
        //当一个类实现类多个接口时候,如果多个接口有用相同的属性,只需要实现一个即可

        //当一个类实现类多个接口时候,如果接口拥有不同的属性,则必须实现所有接口的属性

    }

}
