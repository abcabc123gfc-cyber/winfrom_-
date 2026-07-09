using System;

namespace day12
{
    internal class 继承
    {
        //类的继承: 一个类可以继承另一个类 子类:派生类 父类:基类

        //基类与派生类是相对的概念

        //子类可以拥有父类的属性和方法
        //类只能继承一个基类,权限保持一致,或者父类权限大

        //C#中所有的类直接或间接继承object类                                                                           
    }
    //基类
    class Person
    {
        public string name;
        public string Name { get { return name; } }

        int age;
        public int Age { get { return age; } }
        public char Gender { get; set; }
        public void Show()
        {
            Console.WriteLine("姓名：{0}，年龄：{1}", name, age);
        }
        public Person()
        {
            age = 0;

        }
        public Person(string name, char gender, int age)
        {
            this.name = name;
            Gender = gender;
            this.age = age;
        }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;

        }
        

    }
    //派生类
    class Student : Person
    {
        public string school;
        private int studentID;
        public int StudentID { get { return studentID; } }

        public Student()
        {
            name = "UnKnown";
            studentID = 0000;
        }
        public Student(string name, char gender, string school, int studentID, int age) : base(name, gender, age)
        {
            this.school = school;
            this.studentID = studentID;
        }
        public Student(string name, int age):base(name, age)
        {
         studentID=0000;
        }
        public new void Show()
        {
            Console.WriteLine("姓名：{0}，性别：{1}，年龄：{2}，学号：{3}，学校：{4}", name, Gender, Age, studentID, school);
            
        }
    }

    class Employee : Person
    {
        double Salary;
        public Employee(string name, char Gender, double Salary) : base(name, Gender)
        {
            this.Salary = Salary;

        }
 
        public void ShowDetails()
        {
            Console.WriteLine("姓名：{0}，性别：{1}，薪资：{2}", name, Gender, Salary);
        }

    }
}
