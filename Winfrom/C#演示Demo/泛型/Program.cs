namespace 泛型
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //泛型 generic  泛化数据类型
            //如: 听音乐 : 泛化
            //听具体的音乐: 具体 特化

            //泛型类 不能拿来直接使用 需要创建实例(特化)

            //例子: 水果店 卖水果 ,买水果会不明所以,只有说买具体水果如:苹果 才能 具体实现购买的操作

            Apple apple = new Apple();
            Book book = new Book();
            Box<Apple> box = new Box<Apple>() { Cargo = apple };
            Box<Book> bookBox = new Box<Book>() { Cargo = book };
        }
        class Apple:IUnique<long>
        {
            public string Color { get; set; }
            public long ID { get; set;}
        }
        class Book
        {
            public string Name { get; set; }
        }
        class Box
        {
            public Apple Cargo { get; set; }

        }
        class Box<T>
        {
            public T Cargo { get; set; }
        }
        interface IUnique<T>
        {
            T ID { get; set; }
        }

        /// <summary>
        /// 学生类。
        /// 通过「封闭泛型接口」的方式实现 IUnique：接口的类型参数在类声明时就被
        /// 固定为 long，因此 Student 本身不是泛型类。
        /// </summary>
        class Student : IUnique<long>
        {
            public string Color { get; set; }

            // 隐式实现 IUnique<long>.ID
            // 编译期即确定类型为 long，无装箱、无类型转换，性能最好。
            // 调用方通过 Student 实例可直接访问 ID，也可通过 IUnique<long> 访问。
            public long ID { get; set; }
        }
        /// <summary>
        /// 教师类（泛型）。
        /// 通过「开放泛型接口」的方式实现 IUnique：类型参数 T 由调用方在使用时指定，
        /// Teacher<T> 与 IUnique<T> 的 T 是同一个类型参数。
        /// </summary>
        /// <typeparam name="T">ID 的类型，如 int、long、Guid、string 等</typeparam>
        class Teacher<T> : IUnique<T>
        {
            public string Color { get; set; }

            // 隐式实现 IUnique<T>.ID
            // ID 的具体类型延迟到构造封闭类型时（Teacher<int>、Teacher<Guid>…）才确定。
            // 若需要约束（如只允许值类型），可加 where T : struct。
            public T ID { get; set; }
        }
    }
}
