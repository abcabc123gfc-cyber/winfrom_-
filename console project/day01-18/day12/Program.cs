using System;

namespace day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Man man = new Man(2);
            //Employee employee = new Employee("张三",'男',10000);
            //employee.ShowDetails();
            //ElectricCar electricCar = new ElectricCar("理想", "CC", "9999");
            //electricCar.Show();

            //Book book = new Book(1223, "C#", "张三", 100);
            //AudioBook audioBook = new AudioBook(1223, "C#", "张三", 100, 10);
            //audioBook.Show();

            //Student student = new Student("张三",'男',"市学校",0000,18);
            //student.Show();




            //Calculator.Add(1, 2);
            //Calculator.Add(1,2,3);
            //Calculator.Add(1.1f, 2.2f);
            //Calculator.Add(1,2,3,4,5,6,7,8,9);
            ////----------
            //Console.Clear();
            //Dog dog=new Dog("狗",3);
            //Cat cat = new Cat("猫", 2);
            //Console.WriteLine(dog+cat);

            S1 s1=new S3();
            s1.Show();

        }
        class S1
        {
            private int num;
            public virtual int Num { get { return num; } set { num = value; } }
            public virtual void Show()
            {
                Console.WriteLine("S1");
            }
        }
        class S2 : S1  
        {
            private int num;
            public override int Num { get { return num; } set { num = value; } }
            public override void Show()
            {
                Console.WriteLine("S2");
            }
        }
        class S3 : S1
        {
            public override void Show()
            {
                Console.WriteLine("S3");
            }
        }

    }
}
