using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Book book = new Book(1223, "C#", "张三", 100);
            AudioBook audioBook = new AudioBook(1223, "C#", "张三", 100, 10);
            audioBook.Show();

            //Student student = new Student("张三",'男',"市学校",0000,18);
            //student.Show();




            Calculator.Add(1, 2);
            Calculator.Add(1,2,3);
            Calculator.Add(1.1f, 2.2f);
            Calculator.Add(1,2,3,4,5,6,7,8,9);
            //----------
            Console.Clear();
            Dog dog=new Dog("狗",3);
            Cat cat = new Cat("猫", 2);
            Console.WriteLine(dog+cat);



        }
    }
}
