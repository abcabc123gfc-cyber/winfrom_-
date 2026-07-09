using System;

namespace day15
{
    
    #region 动物继承
    abstract public class Animal
    {
        public string Name { get; set; }
        //多态的实现方式: 1.继承 2.重写 3.抽象类 4.接口
        public int Number { get; set; }
        public int MaxAge { get; set; }
        public int NowAge { get; set; }
        public char Sex { get; set; }


        public Animal()
        {
        }
        public static int operator +(Animal a, Animal b)
        {
            return a.Number + b.Number;
        }

        public Animal(string Name, int Nu)
        {
            this.Name = Name;
            Number = Nu;
        }
        public Animal(string Name, int MaxAge, int NowAge, char Sex)
        {
            this.Name = Name;
            this.MaxAge = MaxAge;
            this.NowAge = NowAge;
            this.Sex = Sex;
        }
        abstract public void Speek();
        //虚方法重载
        public virtual void Eat(string eat)
        {
            Console.WriteLine(eat);
        }
        public virtual void  GetInfo(Animal animal)
        {
            Console.WriteLine($"动物:{animal.Name},当前年龄:{animal.NowAge},最大寿命:{animal.MaxAge}");
        }
    }

    public class Cat : Animal
    {
        public Cat()
        {

        }
        public Cat(string Name, int nu) : base(Name, nu)
        {

        }
        public Cat(string name, int MaxAge, int NowAge, char Sex) : base(name, MaxAge, int.MinValue, Sex)
        {

        }

        public override void Eat(string eat)
        {
            Console.WriteLine($"{Name}吃了{eat}");
        }
        public override void Speek()
        {
            Console.WriteLine("喵喵喵");
        }


    }
    public class Dog : Animal
    {
        public Dog()
        {

        }
        public Dog(string Name, int nu) : base(Name, nu)
        {

        }
        public Dog(string name, int MaxAge, int NowAge, char Sex) : base(name, MaxAge, int.MinValue, Sex)
        {

        }
        public override void Eat(string eat)
        {
            Console.WriteLine($"{Name}吃了{eat}");
        }

        public override void Speek()
        {
            Console.WriteLine("汪汪汪");
        }
    }
    #endregion

    #region 计算图形面积
    class Calculate
    {
        public static void graph(double height, double width, double length)
        {
            Console.WriteLine((length + width) * height / 2);
        }
        public static void graph(double d, double d1)
        {
            Console.WriteLine(d * d1);
        }
        public static void graph(double d)
        {
            Console.WriteLine(d * d);
        }
    }

    #endregion

    #region 图书权限
    abstract class Permission
    {
        public string Degree { get; set; }
        public int MaxBook { get; set; }
        public Permission()
        {

        }
        public Permission(string Degree, int MaxBook)
        {
            this.Degree = Degree;
            this.MaxBook = MaxBook;
        }
        abstract public void Lend();
    }
    /// <summary>
    /// 本科
    /// </summary>
    class Undergraduate : Permission
    {
        public Undergraduate(string Degr, int Max) : base(Degr, Max)
        {

        }
        public override void Lend()
        {
            Console.WriteLine($"{Degree}可以借{MaxBook}本");
        }
    }
    class Master : Permission
    {
        public Master(string Degr, int Max) : base(Degr, Max) { }

        public override void Lend()
        {
            Console.WriteLine($"{Degree}可以借{MaxBook}本");
        }
    }
    class Doctor : Permission
    {
        public Doctor(string Degr, int Max) : base(Degr, Max) { }
        public override void Lend()
        {
            Console.WriteLine($"{Degree}可以借{MaxBook}本");
        }
    }
    #endregion

    #region 登录_支付
    abstract class Logi
    {
        //public Logi(int paa)
        //{

        //}
        //public Logi()
        //{

        //}
        public int Account { get; set; }
        public int Password { get; set; }

        abstract public void Payment(double d);

    }
    class User : Logi
    {
        public User(int Acc, int pass)
        {
            Account = Acc;
            Password = pass;

        }
        public override void Payment(double d)
        {

            Console.WriteLine($"将支付的数字是{d}");
        }
    }

    #endregion
    #region 索引器练习
    class Student1
    {
        public string[] strings;
        public Student1(string[] strings)
        {
            this.strings = strings;
        }
        public string this[int index]
        {
            get => index < strings.Length ? strings[index] : null;
            set
            {
                if (index < strings.Length)
                {
                    strings[index] = value;
                }
                else
                {
                    string[]  newstr = new  string[index+strings.Length];
                    newstr[index] = value;
                    strings.CopyTo(newstr, 0);
                    strings = newstr;
                }
            }
        }
        #endregion
    }
}
