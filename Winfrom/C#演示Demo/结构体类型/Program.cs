using System;

namespace 结构体类型
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stuent stuent = new Stuent();
            stuent.ID = 1;
            //stuent 是局部变量 局部变量分配在 Main函数的栈内存中
            //直接在 栈内存中分配空间 stuent 实例\
            //装箱 将对象 复制一份 stuent 放入堆内存中 obj引用在堆内存的student 实例
            object obj = stuent;
            //将堆内存中的对象 拷贝一份 放入栈内存中
            Stuent stuent1 = (Stuent)obj;
            stuent1.ID = 2;
            //引用类型变量赋值 对 同一个对象的引用

            //值类型变量赋值  是复制一个完整的对象 给 变量
            Console.WriteLine(stuent.ID);
            Console.WriteLine(stuent1.ID);

            //结构体可以实现接口
            stuent.Speak();

            //不能继承派生类 :结构体类型不能由其他类型或者 类 类型 派生而来

            //结构体类型不能有 显式 的无参构造函数

            //允许 显式 的有参构造函数
        }

    }
    interface ISpeak
    {
        void Speak();
    }
    struct Stuent:ISpeak
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Speak()
        {
            Console.WriteLine("Speak 方法");
        }
    }
    struct teacher
    {
        public teacher(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
