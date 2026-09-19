using System;

namespace 接口_依赖注入
{
    internal class 接口隔离_例子3
    {
        public static void Main(string[] args)
        {
            //隐式接口实现 
            //可以通过类实例调用，也可以通过接口引用调用。
            var wk =new WarmKiller();
            //显示接口实现时 无法直接调用Kill() 方法
            //wk.Kill();

            wk.Love();
            IKiller killer = wk;
            killer.Kill();
            ((IKiller)wk).Kill();
        }
        //描述“自定义特性可以用在什么地方”。
        //[AttributeUsage(AttributeTargets.Class)]


        //C# 特有的特性 显式接口实现
        //显式接口实现 : 不但可以接口隔离, 还可以把一些隔离出来的接口隐藏 ,
        //直到显式的使用这种接口类型的变量 , 去引用一个实现了这个接口的夹具体类的实例的时候
        //这个接口中的方法才会被调用 _ 看见

        
    }
    interface IGentleman
    {
        void Love();
    }
    interface IKiller
    {
        void Kill();
    }
    //接口的实现有两种: 显式接口实现 和 隐式接口实现
    //隐式实现是把接口成员实现成类的 public 成员；

    //显式实现是明确写成 接口名.成员名，只能通过接口引用访问，不能通过类实例直接访问。
    class WarmKiller : IGentleman, IKiller
    {
        //隐式接口实现
        //public void Kill()
        //{
        //    Console.WriteLine("Let me kill the enemy");
        //}
        //隐式接口实现
        public void Love()
        {
            Console.WriteLine("I will love you for ervr...");
        }

        //显式接口实现
        //作用: 显式接口实现可以隐藏接口中的方法，直到显式使用这个接口类型的变量，
        //去引用一个实现了这个接口的夹具体类的实例的时候，这个接口中的方法才会被调用。
        void IKiller.Kill()
        {
            Console.WriteLine("Let me kill the enemy");
        }
    }

}
